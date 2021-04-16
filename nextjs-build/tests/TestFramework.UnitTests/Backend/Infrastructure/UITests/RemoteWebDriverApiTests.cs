using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Infrastructure;
using TestFramework.Infrastructure.HttpRequest;
using TestFramework.Infrastructure.UITests;
using TestFramework.Infrastructure.UITests.Dto;
using TestFramework.UnitTests.Resources;
using Xunit;

namespace TestFramework.UnitTests.Backend.Infrastructure.UITests
{
    public class RemoteWebDriverApiTests
    {
        [Fact]
        public void ShouldSeserializeCorrectly()
        {
            // Arrange
            var expected = Encoding.Default.GetString(FileResource.logRequest);

            var logRequest = new LogRequest
            {
                Type = "browser"
            };

            // Act
            var actual = new JsonSerialization().Serialize(logRequest);

            // Assert
            ValidateJson.ShouldBeEquivalent(actual, expected);
        }

        [Fact]
        public void ShouldDeserializeCorrectly()
        {
            // Arrange
            var logResponseBody = Encoding.Default.GetString(FileResource.logResponse);

            var expected = new LogsResponse
            {
                Value = new List<Log>
                {
                    new()
                    {
                        Level = "WARNING",
                        Message = "http://host.docker.internal:5000/_framework/blazor.webassembly.js 0:9804 \"Log Warning\"",
                        Source = "console-api",
                        Timestamp = new DateTime(1618391826681)
                    },
                    new()
                    {
                        Level = "SEVERE",
                        Message = "http://host.docker.internal:5000/_framework/blazor.webassembly.js 0:9804 \"Log Error\"",
                        Source = "console-api",
                        Timestamp = new DateTime(1618391826682)
                    },
                    new()
                    {
                        Level = "SEVERE",
                        Message = "http://host.docker.internal:5000/_framework/blazor.webassembly.js 0:38060",
                        Source = "console-api",
                        Timestamp = new DateTime(1618391826720)
                    }
                }
            };

            // Act
            var actual = new JsonSerialization().Deserialize<LogsResponse>(logResponseBody);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnLogs()
        {
            // Arrange
            var apiOptions = new RemoteWebDriverApiOptions()
            {
                MaxRetries = 0,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 0
            };

            var expectedWarning = new Log()
            {
                Level = "WARNING",
                Message = "warning log",
                Timestamp = DateTime.UtcNow
            };

            var expectedError = new Log()
            {
                Level = "SEVERE",
                Message = "severe log",
                Timestamp = DateTime.UtcNow
            };

            var expectedUnone = new Log()
            {
                Level = "random",
                Message = "random log",
                Timestamp = DateTime.UtcNow
            };

            var httpResponse = new HttpRequestResponse<LogsResponse>
            {
                WasSuccessful = true,
                Resource = new LogsResponse
                {
                    Value = new List<Log> { expectedWarning, expectedError, expectedUnone }
                }
            };

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            httpMock.Setup(x => x.PostAsync<LogRequest, LogsResponse>(It.IsAny<Uri>(), It.IsAny<LogRequest>()))
                .ReturnsAsync(httpResponse);


            var api = mock.Create<RemoteWebDriverApi>();

            // Act
            var actual = await api.GetLogsAsync(string.Empty);

            // Assert
            actual.Should().HaveCount(3);
            
            var actualWarning = actual.First();
            actualWarning.LogLevel.Should().Be(EventLogLevel.Warning);
            actualWarning.Message.Should().Be(expectedWarning.Message);
            actualWarning.Timestamp.Should().Be(expectedWarning.Timestamp);

            var actualError = actual.Skip(1).First();
            actualError.LogLevel.Should().Be(EventLogLevel.Error);
            actualError.Message.Should().Be(expectedError.Message);
            actualError.Timestamp.Should().Be(expectedError.Timestamp);

            var actualUnone = actual.Skip(2).First();
            actualUnone.LogLevel.Should().Be(EventLogLevel.Unnone);
            actualUnone.Message.Should().Be(expectedUnone.Message);
            actualUnone.Timestamp.Should().Be(expectedUnone.Timestamp);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public async Task ShouldRetry_WhenHttpRequestException(int maxRetries)
        {
            // Arrange
            var apiOptions = new RemoteWebDriverApiOptions()
            {
                MaxRetries = maxRetries,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 0
            };

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            httpMock.Setup(x => x.PostAsync<LogRequest, LogsResponse>(It.IsAny<Uri>(), It.IsAny<LogRequest>()))
                .Throws<HttpRequestException>();


            var api = mock.Create<RemoteWebDriverApi>();

            // Act
            await api.GetLogsAsync(string.Empty);

            // Assert
            httpMock.Verify(x => x.PostAsync<LogRequest, LogsResponse>(It.IsAny<Uri>(), It.IsAny<LogRequest>()), 
                Times.Exactly(maxRetries + 1));
        }

        [Fact]
        public async Task ShouldReturnEmptyList_WhenStatusCodeFailure()
            => await ShouldReturnEmptyList(setup => setup.ReturnsAsync(new HttpRequestResponse<LogsResponse>
            {
                WasSuccessful = false
            }));

        [Fact]
        public async Task ShouldReturnEmptyList_WhenHttpRequestExceptionAfterRetries()
            => await ShouldReturnEmptyList(setup => setup.Throws<HttpRequestException>());

        private static async Task ShouldReturnEmptyList(Action<ISetup<IHttpService, Task<HttpRequestResponse<LogsResponse>>>> setup)
        {
            // Arrange
            var apiOptions = new RemoteWebDriverApiOptions()
            {
                MaxRetries = 3,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 0
            };

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            setup(httpMock.Setup(x => x.PostAsync<LogRequest, LogsResponse>(It.IsAny<Uri>(), It.IsAny<LogRequest>())));


            var api = mock.Create<RemoteWebDriverApi>();

            // Act
            var actualResponse = await api.GetLogsAsync(string.Empty);

            // Assert
            actualResponse.Should().BeEmpty();
        }
    }
}
