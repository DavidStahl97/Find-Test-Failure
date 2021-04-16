using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Domain.UITesting.Template
{
    public class UITestCase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri StartUrl { get; set; }

        public TimeSpan DefaultWaitForUIElement { get; set; }
        
        public bool RunsPeriodically { get; set; }

        public virtual ICollection<UIEvent> Events { get; set; }        
    }
}
