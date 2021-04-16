using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class UITestRunWriteEvent : UITestRunUIElementEvent
    {
        public string Input { get; set; }

        public bool GenerateUnique { get; set; }
    }
}
