using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run
{
    public class UITestRun
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public UITestRunState State { get; set; } = UITestRunState.Started;

        public virtual ICollection<UITestRunCase> TestCases { get; set; }
    }
}
