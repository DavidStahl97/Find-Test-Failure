using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunUIElementEventDto : GetUITestRunEventDto
    {
        public TimeSpan WaitForUIElement { get; set; }

        public GetUITestRunUIElementDto UIElement { get; set; }
    }
}
