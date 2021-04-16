using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;

namespace TestFramework.UnitTests.Backend.WebAPI.Controllers
{
    public abstract class HttpMethodWithoutResponse<TController, THandler, TRequest, TResponse, TFailure> : 
        HttpMethodTestBase<TController, THandler, TRequest, TResponse, TFailure>
        where TResponse : new()
        where TController : ControllerBase
    {
        protected abstract Task<IAsyncResult> ExecuteRequest(TController controller);

        protected async Task CheckNotFound(Func<TController, THandler, Task<IActionResult>> executeRequest, TFailure failure)
        {
            // Arrange
            Mock.Mock<IPipelineBuilder>()
                .Setup(CreateMockPipeline())
                .Returns(id => Task.FromResult(new Response<TResponse, TFailure>(failure)));

            // Act
            var response = await executeRequest(Controller, Handler);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}
