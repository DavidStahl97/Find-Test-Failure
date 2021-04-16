using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Dtos.UITests.UITestRuns
{
    public class GetUITestRunDto
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public int CompletedCount { get; set; }

        public int FailedCount { get; set; }

        public int StartedCount { get; set; }

        public int NotStartedCount { get; set; }
    }
}
