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
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUITestCaseCollection
    {
        ValueTask<OneOf<UITestCase, NotFound>> GetByIdAsync(int id);

        void Add(UITestCase testCase);

        void Remove(UITestCase testCase);

        ValueTask<TrueOrFalse> IsNameUsed(string name);

        ValueTask<(IEnumerable<UITestCase> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search);

        Task<OneOf<ICollection<UIEvent>, NotFound>> GetEventsAsync(int testCaseId);

        Task<OneOf<UITestCase, NotFound>> GetByIdWithEventsAsync(int id);

        Task<IEnumerable<UITestCase>> GetByIdsWithEvents(IEnumerable<int> ids, bool shouldTrack = false);

        Task<IEnumerable<int>> GetRunPeriodicallyTests(bool shouldTrack = false);
    }
}
