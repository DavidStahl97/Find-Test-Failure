using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public interface IUITestStarter
    {
        Task<int> ExecuteAsync(IEnumerable<int> selectedTestCases);
    }
}