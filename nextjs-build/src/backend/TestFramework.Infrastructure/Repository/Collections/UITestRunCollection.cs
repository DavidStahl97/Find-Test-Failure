using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class UITestRunCollection : CollectionBase<UITestRun>, IUITestRunCollection
    {
        public UITestRunCollection(RepositoryContext context) : base(context)
        {
        }

        public void Add(UITestRun testRun) => AddEntity(testRun);

        public async ValueTask<(IEnumerable<GetUITestRunDto> Page, int Count)> FindPageAsync(int pageIndex, 
            int pageSize)
        {
            var page = await Context.UITestRuns
                .Include(x => x.TestCases)
                .OrderByDescending(x => x.Start)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(x => new GetUITestRunDto
                {
                    Id = x.Id,
                    Start = x.Start,
                    CompletedCount = x.TestCases.Count(test => test.State == UITestCaseState.Completed),
                    FailedCount = x.TestCases.Count(test => test.State == UITestCaseState.Failure),
                    NotStartedCount = x.TestCases.Count(test => test.State == UITestCaseState.NotStarted),
                    StartedCount = x.TestCases.Count(test => test.State == UITestCaseState.Started),
                })
                .ToListAsync();

            var count = await Context.UITestRuns.CountAsync();

            return (page, count);
        }

        public async Task<OneOf<UITestRun, NotFound>> GetByIdWithTestCasesAsync(int id)
        {
            var testRun = await Context.UITestRuns
                .Include(x => x.TestCases)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (testRun is null)
            {
                return new NotFound();
            }

            return testRun;
        }
    }
}
