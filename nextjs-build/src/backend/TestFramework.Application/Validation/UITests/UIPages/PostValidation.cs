using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation.UITests.UIPages
{
    public class PostValidation : ChangeOrUpdateValidation<PostRequest, ErrorCodes>
    {
        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
