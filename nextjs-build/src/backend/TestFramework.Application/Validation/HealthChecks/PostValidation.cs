using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.HealthChecks
{
    public class PostValidation : ChangeOrCreateValidation<PostRequest, ErrorCodes>
    {
        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
