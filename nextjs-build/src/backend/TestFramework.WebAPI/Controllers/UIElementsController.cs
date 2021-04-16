using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Handler;
using TestFramework.Application.Handler.UITests;
using TestFramework.Application.Handler.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.UITests;
using TestFramework.Application.Types;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.UITests.UIElements;
using TestFramework.Domain.UITesting.Template;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UIElementsController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public UIElementsController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet("{id}", Name = nameof(GetUIElementById))]
        [UnprocessableEntityResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUIElementDto>> GetUIElementById(int id, 
            [FromServices] GetByIdHandler handler)
        {
            var response = await _builder.BuildPipeline(new GetIdValidation(), handler)(id);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound<GetUIElementDto>, 
                    this.ReturnErrorCode<GetUIElementDto>));
        }


        [HttpGet(Name = nameof(GetUIElementPage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetUIElementDto>>> GetUIElementPage(
            int pageIndex, int pageSize, [FromServices] GetPageHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(
                this.ReturnOk, 
                this.ReturnErrorCode<PaginationResult<GetUIElementDto>>);
        }

        [HttpPost(Name = nameof(PostUIElement))]
        [CreatedResponseType]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PostUIElement(ChangeOrCreateUIElemenDto dto, 
            [FromServices] PostHandler handler)
        {
            var request = new PostRequest { Dto = dto };

            var response = await _builder.BuildPipeline(new PostValidation(), handler)(request);
            
            return response.Match(
                created => this.ReturnCreated("api/UIElements/", created), 
                this.ReturnErrorCode);
        }

        [HttpPut("{id}", Name = nameof(PutUIElement))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUIElement(int id, 
            ChangeOrCreateUIElemenDto dto, [FromServices] PutHandler handler)
        {
            var request = new PutRequest
            {
                Id = id,
                Dto = dto
            };

            var response = await _builder.BuildPipeline(new PutValidation(), handler)(request);

            return response.Match(
                this.ReturnOk,
                failure => failure.Match(this.ReturnErrorCode, this.ReturnNotFound));
        }

        [HttpDelete("{id}", Name = nameof(DeleteUIElement))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> DeleteUIElement(int id, [FromServices] DeleteHandler<UIElement> handler)
        {
            var request = new UIElement { Id = id };
            var response = await _builder.BuildPipeline(handler)(request);
            return response.Match(this.ReturnOk, this.ReturnErrorCode);
        }
    }
}
