using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Handler.UITests.UITestRun;
using TestFramework.Application.Repository;

namespace TestFramework.Application.BackgroundTasks.UITests
{
    public class RunTestsPeriodicallyWorker : IIntervalBackgroundWorker
    {
        private readonly IRepository _repository;
        private readonly IUITestStarter _testStarter;

        public RunTestsPeriodicallyWorker(IRepository repository, IUITestStarter testStarter)
        {
            _repository = repository;
            _testStarter = testStarter;
        }

        public async Task ExecuteAsync()
        {
            var periodicallyTests = await _repository.UITestCases.GetRunPeriodicallyTests();

            if (periodicallyTests.Any())
            {
                await _testStarter.ExecuteAsync(periodicallyTests);
            }
        }
    }
}
