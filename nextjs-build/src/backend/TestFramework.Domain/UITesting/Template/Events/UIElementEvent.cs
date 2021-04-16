using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Template.Events
{
    public class UIElementEvent : UIEvent
    {
        public TimeSpan WaitForUIElement { get; set; }

        public bool UseDefaultWaitForUIElement { get; set; }

        public int UIElementId { get; set; }

        public virtual UIElement UIElement { get; set; }
    }
}
