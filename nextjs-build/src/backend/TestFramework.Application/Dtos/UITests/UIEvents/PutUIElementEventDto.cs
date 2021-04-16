using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class PutUIElementEventDto : PutUIEventDto
    {
        public TimeSpan WaitForUIElement { get; set; }

        public bool UseDefaultWaitForUIElement { get; set; }

        public int UIElementId { get; set; }
    }
}
