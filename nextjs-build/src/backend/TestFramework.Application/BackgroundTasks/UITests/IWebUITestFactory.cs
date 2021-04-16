using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.UITests
{
    public interface IWebUITestFactory
    {
        Task StartAsync(Browser browser, Func<IWebUITesting, Task> func);
    }
}
