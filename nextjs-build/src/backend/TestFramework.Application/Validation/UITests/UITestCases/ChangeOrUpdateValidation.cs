using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Contract;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Validation.UITests.UITestCases
{
    public abstract class ChangeOrUpdateValidation<TRequest, TFail> : Validator<TRequest, TFail>
        where TRequest : IChangeOrCreate
    {
        public ChangeOrUpdateValidation()
        {
            RuleFor(x => x.Dto.Name)
                .MaximumLength(Contracts.UITestCases.NameMaxLength)
                .WithErrorCode(ErrorCode.UITestCases_NameMaxLength);

            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.UITestCases_NameIsEmpty);

            RuleFor(x => x.Dto.StartUrl)
                .NotEmpty()
                .WithErrorCode(ErrorCode.UITestCases_StartUrlIsEmpty);
        }
    }
}
