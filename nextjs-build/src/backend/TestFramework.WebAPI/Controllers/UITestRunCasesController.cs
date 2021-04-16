using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunCases;
using TestFramework.Application.Handler.UITests.UITestRunCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Validation;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UITestRunCasesController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public UITestRunCasesController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet("{id}/Events", Name = nameof(GetUITestRunCaseDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<GetUITestRunCaseDetailsDto>> GetUITestRunCaseDetails(int id, 
            [FromServices] GetDetailsHandler handler)
        {
            var response = await _builder.BuildPipeline(new GetIdValidation(), handler)(id);
            return response.Match(
                this.ReturnOk,
                failure => failure.Match(
                    this.ReturnNotFound<GetUITestRunCaseDetailsDto>,
                    this.ReturnErrorCode<GetUITestRunCaseDetailsDto>));
        }
    }
}
