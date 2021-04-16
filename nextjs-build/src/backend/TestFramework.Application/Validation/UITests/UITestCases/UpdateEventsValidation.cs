using FluentValidation;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Contract;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Validation.UITests.UITestCases
{
    public class UpdateEventsValidation : Validator<UpdateEventsRequest, Failure<ErrorCodes, NotFound>>
    {
        public UpdateEventsValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCode.IdLessThen1);

            RuleFor(x => x.Dto.Events.Select(x => x.Step).ToList())
                .Must(x => x.Count == x.Distinct().Count())
                .WithErrorCode(ErrorCode.UITestCases_StepsAreNotUnique);

            RuleForEach(x => x.Dto.Events)
                .ChildRules(x => x.RuleFor(e => e.Name)
                    .NotEmpty()
                    .WithErrorCode(ErrorCode.UIEvents_NameIsEmtpy));

            RuleForEach(x => x.Dto.Events)
                .ChildRules(x => x.RuleFor(e => e.Name)
                    .MaximumLength(Contracts.UIEvents.NameMaxLength)
                    .WithErrorCode(ErrorCode.UIEvents_NameMaxLength));
        }

        protected override Failure<ErrorCodes, NotFound> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
