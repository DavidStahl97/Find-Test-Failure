using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class EditImportFileEvent : EditUIElementEvent
    {
        public override string EditType => "Import File";

        public GetUserFileDto UserFile { get; set; }
    }
}
