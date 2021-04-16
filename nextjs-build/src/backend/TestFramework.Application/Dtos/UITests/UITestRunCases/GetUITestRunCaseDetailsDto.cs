using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Dtos.UITests.UITestRunCases
{
    public class GetUITestRunCaseDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri StartUrl { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public Browser Browser { get; set; }

        public UITestCaseState State { get; set; }

        public IEnumerable<GetUITestRunEventDto> Events { get; set; }
    }
}
