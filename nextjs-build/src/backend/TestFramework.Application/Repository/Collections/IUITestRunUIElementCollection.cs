using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Repository.Collections
{
    public interface IUITestRunUIElementCollection
    {
        void AddRange(IEnumerable<UITestRunUIElement> elements);

        Task<IEnumerable<UITestRunUIElement>> GetRangeByIdsAsync(IEnumerable<int> ids, bool shouldTrack = false);
    }
}
