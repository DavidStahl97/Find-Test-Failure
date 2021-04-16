using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUIEventCollection
    {
        void RemoveRange(IEnumerable<UIEvent> events);

        void AddRange(IEnumerable<UIEvent> events);        
    }
}
