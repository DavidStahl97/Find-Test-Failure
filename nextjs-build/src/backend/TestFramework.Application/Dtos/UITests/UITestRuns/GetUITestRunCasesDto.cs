using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Dtos.UITests.UITestRuns
{
    public class GetUITestRunCasesDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public UITestRunState State { get; set; }

        public IEnumerable<GetUITestRunCaseDto> TestCases { get; set; }
    }
}
