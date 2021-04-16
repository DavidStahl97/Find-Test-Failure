using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
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
    class UITestRunCaseCollection : CollectionBase<UITestRunCase>, IUITestRunCaseCollection
    {
        public UITestRunCaseCollection(RepositoryContext context) : base(context)
        {
        }

        public ValueTask<OneOf<UITestRunCase, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public async Task<OneOf<UITestRunCase, NotFound>> GetByIdWithEventsAsync(int id)
        {
            var testCase = await Context.UITestRunCases
                .Include(x => x.Events)
                .ThenInclude(x => x.Logs)
                .SingleAsync(x => x.Id == id);

            if (testCase is null)
            {
                return new NotFound();
            }

            return testCase;
        }

        public async Task<IEnumerable<TestCasesStatusInfo>> GetStatusInfos(DateTime start, DateTime end)
        {
            var result = await Context.UITestRunCases
                .Where(x => (x.Start >= start && x.Start <= end) || x.Start == null)
                .GroupBy(x => x.State)
                .Select(x => new TestCasesStatusInfo
                {
                    State = x.Key,
                    Count = x.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<UITestRunCase>> GetAutomatedFailedTests(bool shouldBeTracked = false)
            => await Context.UITestRunCases
                    .AddTracking(shouldBeTracked)
                    .Where(x => 
                        x.AutomaticallyStarted && 
                        x.State == UITestCaseState.Failure &&
                        x.FailureSendedState == UITestRunCaseFailureSendedState.NotSended)
                    .ToListAsync();
                    
    }
}
