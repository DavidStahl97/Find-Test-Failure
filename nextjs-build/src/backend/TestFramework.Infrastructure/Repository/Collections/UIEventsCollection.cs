using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UIEventsCollection : CollectionBase<UIEvent>, IUIEventCollection
    {
        public UIEventsCollection(RepositoryContext context) : base(context)
        {
        }

        public void RemoveRange(IEnumerable<UIEvent> events)
            => RemoveRangeEntities(events);

        public void AddRange(IEnumerable<UIEvent> events)
            => AddRangeEntites(events);
    }
}
