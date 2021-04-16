using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetUIElementEventDto : GetUIEventDto
    {
        public TimeSpan WaitForUIElement { get; set; }

        public bool UseDefaultWaitForUIElement { get; set; }

        public GetUIElementFromEvent UIElement { get; set; }
    }
}
