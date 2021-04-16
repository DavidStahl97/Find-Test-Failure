using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UITestCases
{
    public class PutValidation : ChangeOrUpdateValidation<PutRequest, Failure<NotFound, ErrorCodes>>
    {
        protected override Failure<NotFound, ErrorCodes> CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
