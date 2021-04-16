using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Domain.UITesting.Run
{
    public class UITestRunUIElement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FindByMethod FindByMethod { get; set; }

        public string FindBy { get; set; }

        public virtual ICollection<UITestRunClickEvent> ClickEvents { get; set; }

        public virtual ICollection<UITestRunWriteEvent> WriteEvents { get; set; }

        public virtual ICollection<UITestRunMoveToUIElementEvent> MoveToUIElements { get; set; }

        public virtual ICollection<UITestRunClearContentEvent> ClearContentEvents { get; set; }

        public virtual ICollection<UITestRunImportFileEvent> ImportFileEvents { get; set; }

        public const int NameMaxLength = 50;
    }
}
