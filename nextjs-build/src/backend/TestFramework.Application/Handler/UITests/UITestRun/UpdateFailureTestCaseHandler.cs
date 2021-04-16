using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Repository;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class FailureTestCaseRequest : INotification
    {
        public UITestRunCase TestCase { get; init; }
    }

    public class UpdateFailureTestCaseHandler : INotificationHandler<FailureTestCaseRequest>
    {
        private readonly ILogger<UpdateFailureTestCaseHandler> _logger;
        private readonly IRepository _repository;

        public UpdateFailureTestCaseHandler(ILogger<UpdateFailureTestCaseHandler> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(FailureTestCaseRequest notification, CancellationToken cancellationToken)
        {
            var result = await _repository.UITestRunCases.GetByIdWithEventsAsync(notification.TestCase.Id);
            await result.Match(
                testCase => UpdateTestCase(testCase, notification.TestCase),
                notFound =>
                {
                    _logger.LogCritical("Run Test Case not found with id {0}", notification.TestCase.Id);
                    return Task.CompletedTask;
                });
        }

        public async Task UpdateTestCase(UITestRunCase storedTestBase, UITestRunCase testCase)
        {
            storedTestBase.UpdateState(testCase);
            storedTestBase.Events
                .ToList()
                .ForEach(storedEvent =>
                {
                    var updatedEvent = testCase.Events.Single(x => x.Id == storedEvent.Id);
                    storedEvent.UpdateState(updatedEvent);
                });

            await _repository.SaveChangesAsync();
        }
    }
}
