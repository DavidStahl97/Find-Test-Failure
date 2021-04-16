using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation
{
    public abstract class IdValidation<TFailure> : Validator<int, TFailure>
    {
        public IdValidation()
        {
            RuleFor(x => x).GreaterThanOrEqualTo(1)
                .WithErrorCode(ErrorCode.IdLessThen1);
        }
    }
}
