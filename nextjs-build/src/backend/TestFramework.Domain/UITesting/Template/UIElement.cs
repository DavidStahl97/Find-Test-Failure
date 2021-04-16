using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Domain.UITesting.Template
{
    public class UIElement
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FindByMethod FindByMethod { get; set; }

        public string FindBy { get; set; }

        public int PageId { get; set; }

        public Page Page { get; set; }

        public virtual ICollection<ClickEvent> ClickEvents { get; set; }

        public virtual ICollection<WriteEvent> WriteEvents { get; set; }

        public virtual ICollection<MoveToUIElementEvent> MoveToUIElementEvents { get; set; }

        public virtual ICollection<ClearContentEvent> ClearContentEvents { get; set; }     
        
        public virtual ICollection<ImportFileEvent> ImportFileEvents { get; set; }
    }
}
