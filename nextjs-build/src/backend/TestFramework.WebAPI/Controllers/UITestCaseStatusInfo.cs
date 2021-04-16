using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Handler.UITests.UITestRunCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Validation.UITests.UITestRuns;
using TestFramework.Domain.UITesting.Run;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UITestCaseStatusInfo : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public UITestCaseStatusInfo(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet(Name = nameof(GetTestCasesStatusInfos))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<IEnumerable<TestCasesStatusInfo>>> GetTestCasesStatusInfos(
            DateTime start, DateTime end,
            [FromServices] GetStatusInfosHandler handler)
        {
            var request = new GetStatusRequest
            {
                Start = start,
                End = end,
            };

            var response = await _builder.BuildPipeline(new StatusInfosValidation(), handler)(request);

            return response.Match(this.ReturnOk, this.ReturnErrorCode<IEnumerable<TestCasesStatusInfo>>);
        }
    }
}
