using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class UITestRunImportFileEvent : UITestRunUIElementEvent
    {
        public string FileName { get; set; }

        public string StoredFileName { get; set; }
    }
}
