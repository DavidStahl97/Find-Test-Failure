using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class EditWriteEvent : EditUIElementEvent
    {
        public string Input { get; set; }

        public bool GenerateUnique { get; set; }

        public override string EditType => "WriteEvent";
    }
}
