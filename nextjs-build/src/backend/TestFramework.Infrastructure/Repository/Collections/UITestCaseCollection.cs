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
using TestFramework.Domain.UITesting.Template.Events;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UITestCaseCollection : CollectionBase<UITestCase>, IUITestCaseCollection
    {
        public UITestCaseCollection(RepositoryContext context) : base(context)
        {
        }

        public void Add(UITestCase testCase)
            => AddEntity(testCase);

        public ValueTask<OneOf<UITestCase, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public ValueTask<TrueOrFalse> IsNameUsed(string name)
            => AnyAsync(x => x.Name == name);

        public ValueTask<(IEnumerable<UITestCase> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, 
            string search)
            => FindEntityPageAscendingOrderAsync(pageIndex, pageSize, x => x.Name, 
                    search: x => x.Name.Contains(search));

        public async Task<OneOf<ICollection<UIEvent>, NotFound>> GetEventsAsync(int testCaseId)
        {
            var testCase = await Context.UITestCases.Include(x => x.Events)
                .SingleOrDefaultAsync(x => x.Id == testCaseId);

            if (testCase is null)
            {
                return new NotFound();
            }

            return OneOf<ICollection<UIEvent>, NotFound>.FromT0(testCase.Events);
        }

        public async Task<OneOf<UITestCase, NotFound>> GetByIdWithEventsAsync(int id)
        {
            var testCase = await Context.UITestCases
                .Include(x => x.Events)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (testCase is null)
            {
                return new NotFound();
            }

            return testCase;
        }

        public async Task<IEnumerable<UITestCase>> GetByIdsWithEvents(IEnumerable<int> ids, bool shouldTrack = false)
            => await Context.UITestCases
                    .AddTracking(shouldTrack)
                    .Include(x => x.Events)
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();

        public void Remove(UITestCase testCase)
            => RemoveEntity(testCase);

        public async Task<IEnumerable<int>> GetRunPeriodicallyTests(bool shouldTrack = false)
            => await Context.UITestCases
                    .AddTracking(shouldTrack)
                    .Where(x => x.RunsPeriodically)
                    .Select(x => x.Id)
                    .ToListAsync();
    }
}
