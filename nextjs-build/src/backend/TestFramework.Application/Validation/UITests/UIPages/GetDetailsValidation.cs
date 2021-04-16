using FluentValidation;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UIPages
{
    public class GetDetailsValidation : Validator<GetDetailsRequest, Failure<NotFound, ErrorCodes>>
    {
        public GetDetailsValidation()
        {
            RuleFor(x => x.Pagination.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorCode.PageIndexLessThan0);

            RuleFor(x => x.Pagination.PageSize)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.PageSizeLessThen1);

            RuleFor(x => x.Id).GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCode.IdLessThen1);
        }

        protected override Failure<NotFound, ErrorCodes> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
