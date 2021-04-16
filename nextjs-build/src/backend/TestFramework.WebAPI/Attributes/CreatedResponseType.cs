using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.WebAPI.Attributes
{
    public class CreatedResponseType : ProducesResponseTypeAttribute
    {
        public CreatedResponseType()
            : base(typeof(PostResponse), StatusCodes.Status201Created)
        {
        }
    }

    public class PostResponse
    {
        public int Id { get; set; }
    }
}
