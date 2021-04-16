using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class UITestRunUIElementEvent : UITestRunEvent
    {
        public TimeSpan WaitForUIElement { get; set; }

        public bool UseDefaultWaitForUIElement { get; set; }

        public int UITestRunUIElementId { get; set; }

        public virtual UITestRunUIElement UIElement { get; set; }
    }
}
