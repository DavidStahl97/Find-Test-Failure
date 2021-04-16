using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UIElementCollection : CollectionBase<UIElement>, IUIElementCollection
    {
        public UIElementCollection(RepositoryContext context) : base(context)
        {
        }

        public ValueTask<OneOf<UIElement, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public ValueTask<TrueOrFalse> IsNameUsed(string name)
            => AnyAsync(x => x.Name == name);

        public void Add(UIElement element)
            => AddEntity(element);

        public ValueTask<(IEnumerable<UIElement> Page, int Count)> FindPageAsync(int pageIndex, int pageSize)
            => FindEntityPageDescendingOrderAsync(pageIndex, pageSize, x => x.Id);

        public Task<int> GetRangeCountByIdAsync(IEnumerable<int> ids)
            => Context.UIElements.Where(x => ids.Contains(x.Id))
                    .CountAsync();

        public async Task<IEnumerable<UIElement>> GetRangeByIdAsync(IEnumerable<int> ids, bool shouldTrack = false)
            => await Context.UIElements
                    .AddTracking(shouldTrack)
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

        public async Task<IEnumerable<UIElement>> GetRangeByIdWithPageAsync(IEnumerable<int> ids, bool shouldTrack = false)
            => await Context.UIElements
                    .AddTracking(shouldTrack)
                    .Include(x => x.Page)
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

        public void Remove(UIElement element)
            => RemoveEntity(element);
    }
}
