using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UITestRunUIElementCollection : CollectionBase<UITestRunUIElement>, IUITestRunUIElementCollection
    {
        public UITestRunUIElementCollection(RepositoryContext context) : base(context)
        {
        }

        public void AddRange(IEnumerable<UITestRunUIElement> elements)
            => AddRangeEntites(elements);

        public async Task<IEnumerable<UITestRunUIElement>> GetRangeByIdsAsync(IEnumerable<int> ids, bool shouldTrack = false)
            => await Context.UITestRunUIElement
                    .AddTracking(shouldTrack)
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();
    }
}
