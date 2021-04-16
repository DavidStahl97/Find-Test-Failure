using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Template
{
    public class Page
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UIElement> UIElements { get; set; }        
    }
}
