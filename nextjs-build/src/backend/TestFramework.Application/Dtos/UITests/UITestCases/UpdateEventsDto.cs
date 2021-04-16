using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;

namespace TestFramework.Application.Dtos.UITests.UITestCases
{
    public class UpdateEventsDto
    {        
        public IEnumerable<PutUIEventDto> Events { get; set; }
    }
}
