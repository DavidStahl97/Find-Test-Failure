using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUITestRunCollection
    {
        void Add(UITestRun testRun);

        ValueTask<(IEnumerable<GetUITestRunDto> Page, int Count)> FindPageAsync(int pageIndex, int pageSize);

        Task<OneOf<UITestRun, NotFound>> GetByIdWithTestCasesAsync(int id);
    }
}
