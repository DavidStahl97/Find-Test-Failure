using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUITestRunCaseCollection
    {
        Task<OneOf<UITestRunCase, NotFound>> GetByIdWithEventsAsync(int id);

        ValueTask<OneOf<UITestRunCase, NotFound>> GetByIdAsync(int id);

        Task<IEnumerable<TestCasesStatusInfo>> GetStatusInfos(DateTime start, DateTime end);

        Task<IEnumerable<UITestRunCase>> GetAutomatedFailedTests(bool shouldBeTracked = false);
    }
}
