using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class EditClickAtPositionEvent : EditEvent
    {
        public override string EditType => "Click At Current Position";
    }
}
