using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Handler.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Types;
using TestFramework.Application.Validation;
using TestFramework.WebAPI.Controllers;
using Xunit;

namespace TestFramework.UnitTests.Backend.WebAPI.Controllers.UIElements
{
    public class GetUIElementByIdTests
        : HttpMethodWithResponse<UIElementsController, GetByIdHandler, int, GetUIElementDto, Failure<NotFound, ErrorCodes>>
    {
        protected override Expression<Func<IPipelineBuilder, Func<int, Task<Response<GetUIElementDto, Failure<NotFound, ErrorCodes>>>>>> CreateMockPipeline()
            => x => x.BuildPipeline(It.IsAny<GetIdValidation>(), It.IsAny<GetByIdHandler>());
        
        [Fact]
        public override Task ShouldReturnResponse()
            => CheckResponse((controller, handler) => controller.GetUIElementById(0, handler));

        [Fact]
        public override Task ShouldBuildingPipelineCorrectly()
            => CheckPipeline((controller, handler) => controller.GetUIElementById(0, handler),
                mock => mock.Callback<GetIdValidation, GetByIdHandler>((validation, handler) =>
                {
                    validation.Should().NotBeNull();
                    handler.Should().Be(Handler);
                }));

        [Fact]
        public override Task ShouldStartPipelineCorrectly()
            => CheckStartingPiplineParameter((controller, handler) => controller.GetUIElementById(123, handler),
                id => id.Should().Be(123));

        [Fact]
        public override Task ShouldStartingPipelineOnce()
            => CheckStartingOnce((controller, handler) => controller.GetUIElementById(0, handler));

        [Fact]
        public Task ShouldReturnNotFound()
            => CheckNotFound((controller, handler) => controller.GetUIElementById(0, handler), new NotFound());

        [Fact]
        public Task ShouldReturnErrorCodes()
            => CheckErrorCodes((controller, handler) => controller.GetUIElementById(0, handler), 
                new ErrorCodes(ErrorCode.IdLessThen1), x => x.AsT1);
    }
}
