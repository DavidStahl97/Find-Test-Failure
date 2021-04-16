using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Types;
using TestFramework.Contract;
using TestFramework.Domain;

namespace TestFramework.Application.Validation.HealthChecks
{
    public abstract class ChangeOrCreateValidation<TRequest, TFail> : Validator<TRequest, TFail>
            where TRequest : IChangeOrCreate
    {
        public ChangeOrCreateValidation()
        {
            RuleFor(x => x.Dto.Name)
                .MaximumLength(Contracts.HealthChecks.NameMaxLength)
                .WithErrorCode(ErrorCode.HealthChecks_NameMaxLength);

            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HealthChecks_NameIsEmpty);

            RuleFor(x => x.Dto.Url)
                .NotEmpty()
                .WithErrorCode(ErrorCode.HealthChecks_UriIsEmpty);
        }
    }
}
