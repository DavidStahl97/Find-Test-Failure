using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation
{
    public class PaginationValidation : Validator<PaginationRequest, ErrorCodes>
    {
        public PaginationValidation()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorCode.PageIndexLessThan0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithErrorCode(ErrorCode.PageSizeLessThen1);
        }

        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
