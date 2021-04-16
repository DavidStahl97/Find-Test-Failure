using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Types;

namespace TestFramework.Application.Requests.UITests.UITestCases
{
    public class UpdateEventsRequest
    {
        public int Id { get; init; }

        public UpdateEventsDto Dto { get; init; }
    }
}
