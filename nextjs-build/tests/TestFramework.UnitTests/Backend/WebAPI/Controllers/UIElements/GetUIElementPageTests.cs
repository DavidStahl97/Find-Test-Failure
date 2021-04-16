using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Handler.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;
using TestFramework.Application.Validation;
using TestFramework.WebAPI.Controllers;
using Xunit;

namespace TestFramework.UnitTests.Backend.WebAPI.Controllers.UIElements
{
    public class GetUIElementPageTests : HttpMethodWithResponse<UIElementsController, GetPageHandler, PaginationRequest, PaginationResult<GetUIElementDto>, ErrorCodes>
    {
        [Fact]
        public override Task ShouldBuildingPipelineCorrectly()
            => CheckPipeline((controller, handler) => controller.GetUIElementPage(0, 0, handler),
                mock => mock.Callback<PaginationValidation, GetPageHandler>((validation, handler) =>
                {
                    validation.Should().NotBeNull();
                    handler.Should().Be(Handler);
                }));

        [Fact]
        public override Task ShouldReturnResponse()
            => CheckResponse((controller, handler) => controller.GetUIElementPage(0, 0, handler));

        [Fact]
        public override Task ShouldStartingPipelineOnce()
            => CheckStartingOnce((controller, handler) => controller.GetUIElementPage(0, 0, handler));

        [Fact]
        public override Task ShouldStartPipelineCorrectly()
            => CheckStartingPiplineParameter((controller, handler) => controller.GetUIElementPage(10, 50, handler), request =>
            {
                request.PageIndex.Should().Be(10);
                request.PageSize.Should().Be(50);
            });

        [Fact]
        public Task ShouldReturnErrorCodes()
            => CheckErrorCodes((controller, handler) => controller.GetUIElementPage(0, 0, handler),
                new ErrorCodes(ErrorCode.IdLessThen1), x => x);

        protected override Expression<Func<IPipelineBuilder, Func<PaginationRequest, Task<Response<PaginationResult<GetUIElementDto>, ErrorCodes>>>>> CreateMockPipeline()
            => x => x.BuildPipeline(It.IsAny<PaginationValidation>(), It.IsAny<GetPageHandler>());
    }
}
