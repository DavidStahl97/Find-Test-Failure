using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Handler;
using TestFramework.Application.Handler.UITests.UITestCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.UITests.UITestCases;
using TestFramework.Domain.UITesting.Template;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UITestCasesController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public UITestCasesController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet(Name = nameof(GetUITestPage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetUITestCaseDto>>> GetUITestPage(
            int pageIndex, int pageSize, string search, [FromServices] GetPageHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search,
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                this.ReturnErrorCode<PaginationResult<GetUITestCaseDto>>);
        }

        [HttpGet("{id}", Name = nameof(GetUITestDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<GetUITestCaseDetailsDto>> GetUITestDetails(int id,
            [FromServices] GetDetailsHandler handler)
        {
            var response = await _builder.BuildPipeline(new GetIdValidation(), handler)(id);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound<GetUITestCaseDetailsDto>,
                    this.ReturnErrorCode<GetUITestCaseDetailsDto>));
        }

        [HttpPost(Name = nameof(PostUITestCase))]
        [CreatedResponseType]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PostUITestCase(ChangeOrCreateUITestCaseDto dto,
            [FromServices] PostHandler handler)
        {
            var request = new PostRequest { Dto = dto };

            var response = await _builder.BuildPipeline(new PostValidation(), handler)(request);

            return response.Match(
                created => this.ReturnCreated("api/UITestCases/", created),
                this.ReturnErrorCode);
        }

        [HttpPut("{id}", Name = nameof(PutUITestCase))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PutUITestCase(int id, ChangeOrCreateUITestCaseDto dto, 
            [FromServices] PutHandler handler)
        {
            var request = new PutRequest
            {
                Dto = dto,
                Id = id,
            };

            var response = await _builder.BuildPipeline(new PutValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound,
                    this.ReturnErrorCode));
        }

        [HttpPut("{id}/UIEvents", Name = nameof(PutUITestCaseEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PutUITestCaseEvents(int id, UpdateEventsDto dto,
            [FromServices] UpdateEventsHandler handler)
        {
            var request = new UpdateEventsRequest
            {
                Id = id,
                Dto = dto,
            };

            var response = await _builder.BuildPipeline(new UpdateEventsValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(this.ReturnErrorCode, this.ReturnNotFound));
        }

        [HttpDelete("{id}", Name = nameof(DeleteTestCase))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> DeleteTestCase(int id, [FromServices] DeleteHandler<UITestCase> handler)
        {
            var testCase = new UITestCase { Id = id };
            var response = await _builder.BuildPipeline(handler)(testCase);
            return response.Match(this.ReturnOk, this.ReturnErrorCode);
        }
    }
}
