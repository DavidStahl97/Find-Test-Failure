using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Template.Events
{
    public abstract class UIEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Step { get; set; }        

        public int UITestCaseId { get; set; }

        public virtual UITestCase UITestCase { get; set; }        
    }
}
