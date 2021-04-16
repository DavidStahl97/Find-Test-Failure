using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Handler.UITests.UITestRun;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.UITests.UITestRuns;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UITestRunsController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public UITestRunsController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpPost(Name = nameof(PostTestRun))]
        [CreatedResponseType]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PostTestRun(PostUITestRunDto dto, [FromServices] PostHandler handler)
        {
            var response = await _builder.BuildPipeline(new PostValidation(), handler)(dto);

            return response.Match(
                created => this.ReturnCreated("api/UITestRuns/", created),
                this.ReturnErrorCode);
        }

        [HttpGet(Name = nameof(GetUITestRunDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetUITestRunDto>>> GetUITestRunPage(int pageIndex, int pageSize,
            [FromServices] GetPageHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                this.ReturnErrorCode<PaginationResult<GetUITestRunDto>>);
        }

        [HttpGet("{id}/TestCases", Name = nameof(GetUITestRunCases))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<GetUITestRunCasesDto>> GetUITestRunCases(int id, [FromServices] GetTestCasesHandler handler)
        {
            var response = await _builder.BuildPipeline(new GetIdValidation(), handler)(id);
            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound<GetUITestRunCasesDto>, 
                    this.ReturnErrorCode<GetUITestRunCasesDto>));
        }
    }
}
