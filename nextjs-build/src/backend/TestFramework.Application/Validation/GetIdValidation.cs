using FluentValidation;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation
{
    public class GetIdValidation : IdValidation<Failure<NotFound, ErrorCodes>>
    {
        protected override Failure<NotFound, ErrorCodes> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
