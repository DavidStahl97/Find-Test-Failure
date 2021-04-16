using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Types;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Attributes
{
    public class UnprocessableEntityResponseType : ProducesResponseTypeAttribute
    {
        public UnprocessableEntityResponseType()
            : base(typeof(ErrorCodesResult), StatusCodes.Status422UnprocessableEntity)
        {
        }
    }
}
