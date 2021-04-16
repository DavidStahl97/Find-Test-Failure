using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UITestRuns
{
    public class StatusInfosValidation : Validator<GetStatusRequest, ErrorCodes>
    {
        public StatusInfosValidation()
        {
            RuleFor(x => x.Start)
                .NotNull()
                .WithErrorCode(ErrorCode.UITestCaseStatusInfo_StartIsEmpty);

            RuleFor(x => x.End)
                .NotNull()
                .WithErrorCode(ErrorCode.UITestCaseStatusInfo_EndIsEmpty);

            RuleFor(x => x.End)
                .GreaterThanOrEqualTo(x => x.Start)
                .WithErrorCode(ErrorCode.UITestCaseStatusInfo_EndLessThenStart);
        }

        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
