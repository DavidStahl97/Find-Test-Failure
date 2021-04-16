using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Handler;
using TestFramework.Application.Handler.UITests.UIPages;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.UITests.UIPages;
using TestFramework.Domain.UITesting.Template;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIPagesController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;
        private readonly IRepository _repository;

        public UIPagesController(IPipelineBuilder builder, IRepository repository)
        {
            _builder = builder;
            _repository = repository;
        }

        [HttpGet("UIElements", Name = nameof(GetAllPages))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllPageDto>> GetAllPages()
        {
            return await _repository.UIPages.GetAllPages();
        }

        [HttpGet("{id}", Name = nameof(GetUIPageById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUIPageDto>> GetUIPageById(int id, 
            [FromServices] GetByIdHandler handler)
        {
            var response = await _builder.BuildPipeline(handler)(id);
            return response.Match(this.ReturnOk, this.ReturnNotFound<GetUIPageDto>);
        }

        [HttpGet(Name = nameof(GetUIPage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetUIPageDto>>> GetUIPage(int pageIndex, int pageSize, string search,
            [FromServices] GetPagesHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(this.ReturnOk, 
                this.ReturnErrorCode<PaginationResult<GetUIPageDto>>);
        }

        [HttpGet("{id}/UIElements", Name = nameof(GetUIPageDetails))]
        public async Task<ActionResult<GetUIPageDetailsDto>> GetUIPageDetails(int id, int pageIndex, int pageSize, 
            string search, [FromServices] GetPageDetailsHandler handler)
        {
            var request = new GetDetailsRequest
            {
                Id = id,
                Pagination = new PaginationRequest
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Search = search,
                }
            };

            var response = await _builder.BuildPipeline(new GetDetailsValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound<GetUIPageDetailsDto>,
                    this.ReturnErrorCode<GetUIPageDetailsDto>));
        }

        [HttpPost(Name = nameof(PostUIPage))]
        [CreatedResponseType]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PostUIPage(ChangeOrCreateUIPageDto dto, [FromServices] PostHandler handler)
        {
            var request = new PostRequest
            {
                Dto = dto
            };

            var response = await _builder.BuildPipeline(new PostValidation(), handler)(request);

            return response.Match(
                created => this.ReturnCreated("api/UIPages/", created),
                this.ReturnErrorCode);
        }

        [HttpPut("{id}", Name = nameof(PutUIPage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PutUIPage(int id, ChangeOrCreateUIPageDto dto, [FromServices] PutHandler handler)
        {
            var request = new PutRequest
            {
                Dto = dto,
                Id = id
            };

            var response = await _builder.BuildPipeline(new PutValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound,
                    this.ReturnErrorCode));
        }

        [HttpDelete("{id}", Name = nameof(DeleteUIPage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> DeleteUIPage(int id, [FromServices] DeleteHandler<Page> handler)
        {
            var page = new Page { Id = id };
            var response = await _builder.BuildPipeline(handler)(page);
            return response.Match(this.ReturnOk, this.ReturnErrorCode);
        }
    }
}
