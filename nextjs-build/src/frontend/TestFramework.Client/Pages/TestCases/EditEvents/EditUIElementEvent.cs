using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public abstract class EditUIElementEvent : EditEvent
    {
        public TimeSpan WaitForUIElement { get; set; }

        public bool UseDefaultWaitForUIElement { get; set; }

        public SelectedUIElementData SelectedUIElement { get; set; }
    }
}
