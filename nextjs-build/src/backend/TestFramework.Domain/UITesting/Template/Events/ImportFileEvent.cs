using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Template.Events
{
    public class ImportFileEvent : UIElementEvent
    {
        public int UserFileId { get; set; }

        public UserFile UserFile { get; set; }
    }
}
