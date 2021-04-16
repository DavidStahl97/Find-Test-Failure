using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;
using TestFramework.Contract;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Validation.UITests.UIPages
{
    public abstract class ChangeOrUpdateValidation<TRequest, TFail> : Validator<TRequest, TFail>
        where TRequest : IChangeOrCreate
    {
        public ChangeOrUpdateValidation()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.UIPages_NameIsEmpty);

            RuleFor(x => x.Dto.Name)
                .MaximumLength(Contracts.UIPages.NameMaxLength)
                .WithErrorCode(ErrorCode.UIPages_NameMaxLength);
        }
    }
}
