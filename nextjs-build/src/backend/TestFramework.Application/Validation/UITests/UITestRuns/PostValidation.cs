using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Extensions;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UITestRuns
{
    public class PostValidation : Validator<PostUITestRunDto, ErrorCodes>
    {
        public PostValidation()
        {
            RuleFor(x => x.SelectedTestCases)
                .NotEmpty()
                .WithErrorCode(ErrorCode.UITestRuns_TestCasesAreEmpty);
        }

        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
