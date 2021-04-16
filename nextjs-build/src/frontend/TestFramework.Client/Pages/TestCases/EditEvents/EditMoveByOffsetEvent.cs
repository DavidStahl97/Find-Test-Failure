using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class EditMoveByOffsetEvent : EditEvent
    {
        public override string EditType => "Move By Offset";

        public int OffsetX { get; set; }

        public int OffsetY { get; set; }
    }
}
