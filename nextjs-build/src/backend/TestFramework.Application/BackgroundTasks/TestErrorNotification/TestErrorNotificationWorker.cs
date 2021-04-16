using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.BackgroundTasks.TestErrorNotification
{
    public class TestErrorNotificationWorker : IIntervalBackgroundWorker
    {
        private readonly ITestErrorNotifyService _notifyService;
        private readonly ILogger<TestErrorNotificationWorker> _logger;
        private readonly IRepository _repository;

        public TestErrorNotificationWorker(ITestErrorNotifyService notifyService, ILogger<TestErrorNotificationWorker> logger, IRepository repository)
        {
            _notifyService = notifyService;
            _logger = logger;
            _repository = repository;
        }

        public async Task ExecuteAsync()
        {
            var failedTestCases = await _repository.UITestRunCases.GetAutomatedFailedTests(shouldBeTracked: true);

            foreach (var testCase in failedTestCases)
            {
                _logger.WriteInformation("Send UI Test Failure to Teams",
                    (nameof(UITestRunCase.Id), testCase.Id));

                var success = await _notifyService.NotifyAsync(testCase);
                success.Match(
                    t => testCase.FailureSendedState = UITestRunCaseFailureSendedState.Sended,
                    f => testCase.FailureSendedState = UITestRunCaseFailureSendedState.SendingFailed);
            }

            await _repository.SaveChangesAsync();
        }
    }
}