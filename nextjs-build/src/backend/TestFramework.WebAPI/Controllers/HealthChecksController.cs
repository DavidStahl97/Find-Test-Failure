using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.HealthChecks;
using TestFramework.Application.Handler;
using TestFramework.Application.Handler.HealthChecks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.HealthChecks;
using TestFramework.Domain;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public HealthChecksController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet(Name = nameof(GetHealthCheckPageAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetHealthCheckDto>>> GetHealthCheckPageAsync(int pageIndex, 
            int pageSize, string search, [FromServices] GetPageHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(this.ReturnOk, this.ReturnErrorCode<PaginationResult<GetHealthCheckDto>>);
        }

        [HttpPost(Name = nameof(PostHealthCheck))]
        [CreatedResponseType]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PostHealthCheck(ChangeOrCreateHealthCheckDto dto,
            [FromServices] PostHandler handler)
        {
            var request = new PostRequest { Dto = dto };

            var response = await _builder.BuildPipeline(new PostValidation(), handler)(request);

            return response.Match(
                created => this.ReturnCreated("api/HealthChecks/", created),
                this.ReturnErrorCode);
        }

        [HttpPut("{id}", Name = nameof(PutHealthCheck))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> PutHealthCheck(int id, ChangeOrCreateHealthCheckDto dto,
            [FromServices] PutHandler handler)
        {
            var request = new PutRequest { Id = id, Dto = dto };

            var response = await _builder.BuildPipeline(new PutValidation(), handler)(request);

            return response.Match(this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound,
                    this.ReturnErrorCode));
        }

        [HttpDelete("{id}", Name = nameof(DeleteHealthCheck))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> DeleteHealthCheck(int id, [FromServices] DeleteHandler<HealthCheck> handler)
        {
            var healthCheck = new HealthCheck { Id = id };
            var response = await _builder.BuildPipeline(handler)(healthCheck);
            return response.Match(this.ReturnOk, this.ReturnErrorCode);
        }
    }
}
