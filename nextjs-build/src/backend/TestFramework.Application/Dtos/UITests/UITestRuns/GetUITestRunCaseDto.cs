using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Dtos.UITests.UITestRuns
{
    public class GetUITestRunCaseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public Browser Browser { get; set; }

        public UITestCaseState State { get; set; }
    }
}
