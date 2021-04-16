using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Types;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.UnitTests.Backend.WebAPI.Controllers
{
    public abstract class HttpMethodWithResponse<TController, THandler, TRequest, TResponse, TFailure>
        : HttpMethodTestBase<TController, THandler, TRequest, TResponse, TFailure>
        where TResponse : new()
        where TController : ControllerBase
    {
        public abstract Task ShouldReturnResponse();

        protected async Task CheckResponse(Func<TController, THandler, Task<ActionResult<TResponse>>> executeRequest)
        {
            // Arrange
            var expectedResponse = CreateResponse();

            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(x => Task.FromResult(new Response<TResponse, TFailure>(expectedResponse)));

            // Act
            var result = await executeRequest(Controller, Handler);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>()
                .Subject.Value.Should().Be(expectedResponse);
        }

        protected async Task CheckNotFound(Func<TController, THandler, Task<ActionResult<TResponse>>> executeRequest, TFailure failure)
        {
            // Arrange
            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(id => Task.FromResult(new Response<TResponse, TFailure>(failure)));

            // Act
            var response = await executeRequest(Controller, Handler);
            

            // Assert
            response.Result.Should().BeOfType<NotFoundResult>();
        }

        protected async Task CheckErrorCodes(Func<TController, THandler, Task<ActionResult<TResponse>>> executeRequest, 
            TFailure failure, Func<TFailure, ErrorCodes> convert)
        {
            // Arrange
            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(id => Task.FromResult(new Response<TResponse, TFailure>(failure)));

            // Act
            var response = await executeRequest(Controller, Handler);

            // Assert
            var errorCodes = response.Result.Should().BeOfType<UnprocessableEntityObjectResult>()
                .Subject.Value.Should().BeOfType<ErrorCodesResult>()
                .Subject.ErrorCodes.Should().BeEquivalentTo(convert(failure));
        }

        protected TResponse CreateResponse() => Fixture.Create<TResponse>();
    }
}
