using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class UITestRunMoveByOffsetEvent : UITestRunEvent
    {
        public int OffsetX { get; set; }

        public int OffsetY { get; set; }
    }
}
