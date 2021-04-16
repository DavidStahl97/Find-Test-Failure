using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Domain
{
    public class UserFile
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string StoredFileName { get; set; }

        public long FileSize { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public virtual ICollection<ImportFileEvent> ImportFileEvents { get; set; }
    }
}
