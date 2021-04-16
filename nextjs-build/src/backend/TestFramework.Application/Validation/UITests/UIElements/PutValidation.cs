using FluentValidation;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.UITests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UIElements
{
    public class PutValidation : ChangeOrUpdateValidation<PutRequest, Failure<ErrorCodes, NotFound>>
    {
        public PutValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCode.IdLessThen1);
        }

        protected override Failure<ErrorCodes, NotFound> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
