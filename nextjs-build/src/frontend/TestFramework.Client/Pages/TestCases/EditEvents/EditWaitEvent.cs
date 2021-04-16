using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class EditWaitEvent : EditEvent
    {
        public TimeSpan Ticks { get; set; }

        public override string EditType => "Wait Event";
    }
}
