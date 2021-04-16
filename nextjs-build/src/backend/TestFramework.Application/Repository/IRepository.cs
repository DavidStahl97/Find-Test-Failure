using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;

namespace TestFramework.Application.Repository
{
    public interface IRepository
    {
        IUIElementCollection UIElements { get; }

        IUITestCaseCollection UITestCases { get; }

        IUIEventCollection UIEvents { get; }

        IUITestRunCollection UITestRuns { get; }

        IUITestRunUIElementCollection UITestRunUIElements { get; }

        IUITestRunEventCollection UITestRunEvents { get; }

        IUITestRunCaseCollection UITestRunCases { get; }

        IUIPageCollection UIPages { get; }

        IHealthCheckCollection HealthChecks { get; }

        IUserFileCollection UserFiles { get; }

        Task SaveChangesAsync();

        Task<TrueOrFalse> TryDeleteEntityAsync<TEntity>(TEntity entity);
    }
}
