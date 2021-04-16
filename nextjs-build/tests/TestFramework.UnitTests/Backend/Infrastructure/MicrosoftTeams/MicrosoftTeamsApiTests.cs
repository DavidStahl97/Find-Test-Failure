using Autofac;
using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Infrastructure;
using TestFramework.Infrastructure.HttpRequest;
using TestFramework.Infrastructure.MicrosoftTeams;
using TestFramework.Infrastructure.MicrosoftTeams.Dto;
using TestFramework.UnitTests.Resources;
using Xunit;

namespace TestFramework.UnitTests.Backend.Infrastructure.MicrosoftTeams
{
    public class MicrosoftTeamsApiTests
    {
        [Fact]
        public void ShouldSerializeCorrectly()
        {
            // Arrange
            var expected = Encoding.Default.GetString(FileResource.teamsCardBody);

            var card = new Card
            {
                Summary = "UI Test Error",
                ThemeColor = "0076D7",
                Sections = new List<Section>
                {
                    new Section
                    {
                        ActivityTitle = "UI Test Error",
                        Facts = new List<Fact>
                        {
                            new Fact { Name = "UI Test", Value = "Unassigned" },
                            new Fact { Name = "Started", Value = "Mon May 01 2017 17:07:18 GMT-0700 (Pacific Daylight Time)"},
                            new Fact { Name = "Browser", Value = "Edge" }
                        }
                    }
                },
                PotentialAction = new List<CardAction>
                {
                    new CardAction
                    {
                        Name = "Link",
                        Targets = new List<Target>
                        {
                            new Target
                            {
                                Uri = "https://docs.microsoft.com/outlook/actionable-messages"
                            }
                        }
                    }
                }
            };

            // Act
            var actual = new JsonSerialization().Serialize(card);

            // Assert
            ValidateJson.ShouldBeEquivalent(actual, expected);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        public async Task ShouldRetry_WhenTooManyRequests(int expectedMaxRetries)
        {
            await TestRetries(setup => setup.ReturnsAsync(new HttpRequestResponse 
            { 
                StatusCode = HttpStatusCode.TooManyRequests
            }), expectedMaxRetries);    
        }

        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        public async Task ShouldRetry_WhenHttpRequestException(int expectedMaxRetries)
            => await TestRetries(setup => setup.Throws<HttpRequestException>(), expectedMaxRetries);

        [Fact]
        public async Task ShouldReturnFalse_WhenTooManyRequests()
            => await ShouldReturnFalse(setup => setup.ReturnsAsync(new HttpRequestResponse
            {
                StatusCode = HttpStatusCode.TooManyRequests
            }));

        [Fact]
        public async Task ShouldReturnFalse_WhenHttpRequestException()
            => await ShouldReturnFalse(setup => setup.Throws<HttpRequestException>());

        [Fact]
        public async Task ShouldReturnTrue_IfHttpRequestWasSuccessful()
        {
            // Arrange
            var apiOptions = new MicrosoftTeamsApiOptions()
            {
                MaxRetries = 0,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 0
            };

            var card = new Card();

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            httpMock.Setup(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<Card>()))
                .ReturnsAsync(new HttpRequestResponse { WasSuccessful = true });


            var api = mock.Create<MicrosoftTeamsApi>();

            // Act
            var actual = await api.PostMessage(card);

            // Assert
            actual.IsT0.Should().BeTrue();
        }


        private static async Task ShouldReturnFalse(Action<ISetup<IHttpService, Task<HttpRequestResponse>>> httpResult)
        {
            // Arrange
            var apiOptions = new MicrosoftTeamsApiOptions()
            {
                MaxRetries = 0,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 0
            };

            var card = new Card();

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            httpResult(httpMock.Setup(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<Card>())));


            var api = mock.Create<MicrosoftTeamsApi>();

            // Act
            var actual = await api.PostMessage(card);

            // Assert
            actual.IsT1.Should().BeTrue();
        }

        private static async Task TestRetries(Action<ISetup<IHttpService, Task<HttpRequestResponse>>> httpResult, 
            int expectedMaxRetries)
        {
            // Arrange
            var apiOptions = new MicrosoftTeamsApiOptions()
            {
                MaxRetries = expectedMaxRetries,
                Uri = new Uri("https://google.com"),
                WaitInMilliseconds = 1000
            };

            var card = new Card();

            using var mock = AutoMock.GetLoose(container =>
                container.RegisterInstance(apiOptions));

            var httpMock = mock.Mock<IHttpService>();
            httpResult(httpMock.Setup(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<Card>())));


            var api = mock.Create<MicrosoftTeamsApi>();

            // Act
            await api.PostMessage(card);

            // Assert
            httpMock.Verify(x => x.PostAsync(It.IsAny<Uri>(), It.IsAny<Card>()), Times.Exactly(expectedMaxRetries + 1));
        }
    }
}