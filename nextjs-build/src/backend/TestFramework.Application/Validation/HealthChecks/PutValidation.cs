using FluentValidation;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.HealthChecks
{
    public class PutValidation : ChangeOrCreateValidation<PutRequest, Failure<NotFound, ErrorCodes>>
    {
        public PutValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCode.IdLessThen1);
        }

        protected override Failure<NotFound, ErrorCodes> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
