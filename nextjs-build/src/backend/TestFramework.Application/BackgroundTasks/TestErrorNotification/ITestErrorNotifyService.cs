using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application
{
    public interface ITestErrorNotifyService
    {
        Task<TrueOrFalse> NotifyAsync(UITestRunCase testCase);
    }
}
