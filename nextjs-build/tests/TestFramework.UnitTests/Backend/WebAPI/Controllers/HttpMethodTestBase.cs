using Autofac.Extras.Moq;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;

namespace TestFramework.UnitTests.Backend.WebAPI.Controllers
{
    public abstract class HttpMethodTestBase<TController, THandler, TRequest, TResponse, TFailure>
        where TResponse : new()
        where TController : ControllerBase
    {
        public HttpMethodTestBase()
        {
            Mock = AutoMock.GetLoose();

            Handler = Mock.Create<THandler>();
            Controller = Mock.Create<TController>();
        }

        protected IFixture Fixture { get; } = new Fixture();

        protected TController Controller { get; }

        protected THandler Handler { get; }

        protected AutoMock Mock { get; }

        protected abstract Expression<Func<IPipelineBuilder, Func<TRequest, Task<Response<TResponse, TFailure>>>>> CreateMockPipeline();

        public abstract Task ShouldBuildingPipelineCorrectly();

        public abstract Task ShouldStartPipelineCorrectly();

        public abstract Task ShouldStartingPipelineOnce();

        protected async Task CheckPipeline(Func<TController, THandler, Task> executeRequest,
            Action<IReturnsResult<IPipelineBuilder>> checkParameters)
        {
            // Arrange
            var mockedPipeline = Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(id => Task.FromResult(new Response<TResponse, TFailure>(new TResponse())));

            // Assert
            checkParameters(mockedPipeline);

            // Act
            await executeRequest(Controller, Handler);
        }

        protected async Task CheckStartingPiplineParameter(Func<TController, THandler, Task> executeRequest,
            Action<TRequest> checkRequest)
        {
            // Arrange
            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(request =>
                {
                    // Assert
                    checkRequest(request);
                    return Task.FromResult(new Response<TResponse, TFailure>(new TResponse()));
                });

            // Act
            await executeRequest(Controller, Handler);
        }

        protected async Task CheckStartingOnce(Func<TController, THandler, Task> executeRequest)
        {
            // Arrange
            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(id => Task.FromResult(new Response<TResponse, TFailure>(new TResponse())));

            // Act
            await executeRequest(Controller, Handler);

            // Assert
            Mock.Mock<IPipelineBuilder>()
                .Verify(CreateMockPipeline(), Times.Once);
        }
    }
}
