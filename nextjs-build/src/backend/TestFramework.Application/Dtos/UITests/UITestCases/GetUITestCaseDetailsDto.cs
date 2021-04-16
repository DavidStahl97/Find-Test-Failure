using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;

namespace TestFramework.Application.Dtos.UITests.UITestCases
{
    public class GetUITestCaseDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri StartUrl { get; set; }

        public TimeSpan DefaultWaitForUIElement { get; set; }

        public IEnumerable<GetUIEventDto> Events { get; set; }
    }
}
