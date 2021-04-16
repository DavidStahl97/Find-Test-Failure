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
using TestFramework.Domain.UITesting;

namespace TestFramework.Application.Validation.UITests.UIElements
{
    public class PostValidation : ChangeOrUpdateValidation<PostRequest, ErrorCodes>
    {
        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
