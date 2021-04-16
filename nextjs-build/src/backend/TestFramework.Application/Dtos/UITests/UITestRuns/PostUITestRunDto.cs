using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRuns
{
    public class PostUITestRunDto
    {
        public IEnumerable<int> SelectedTestCases { get; set; }
    }
}
