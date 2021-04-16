using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUIElementCollection
    {
        void Add(UIElement element);

        void Remove(UIElement element);

        ValueTask<OneOf<UIElement, NotFound>> GetByIdAsync(int id);

        ValueTask<TrueOrFalse> IsNameUsed(string name);

        ValueTask<(IEnumerable<UIElement> Page, int Count)> FindPageAsync(int pageIndex, int pageSize);

        Task<int> GetRangeCountByIdAsync(IEnumerable<int> ids);

        Task<IEnumerable<UIElement>> GetRangeByIdAsync(IEnumerable<int> ids, bool shouldTrack = false);

        Task<IEnumerable<UIElement>> GetRangeByIdWithPageAsync(IEnumerable<int> ids, bool shouldTrack = false);
    }
}
