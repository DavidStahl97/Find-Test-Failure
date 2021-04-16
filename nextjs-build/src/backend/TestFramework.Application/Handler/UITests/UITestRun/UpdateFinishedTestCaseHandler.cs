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
    public class UpdateFinishedTestCaseRequest : INotification
    {
        public UITestRunCase TestCase { get; init; }
    }

    public class UpdateFinishedTestCaseHandler : INotificationHandler<UpdateFinishedTestCaseRequest>
    {
        private readonly ILogger<UpdateTestEventHandler> _logger;
        private readonly IRepository _repository;

        public UpdateFinishedTestCaseHandler(ILogger<UpdateTestEventHandler> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(UpdateFinishedTestCaseRequest notification, CancellationToken cancellationToken)
        {
            var result = await _repository.UITestRunCases.GetByIdAsync(notification.TestCase.Id);
            await result.Match(
                stored => UpdateTestCase(stored, notification.TestCase),
                notFound =>
                {
                    _logger.LogCritical("Test Case not found with Id {0}", notification.TestCase.Id);
                    return Task.CompletedTask;
                });
        }

        private async Task UpdateTestCase(UITestRunCase stored, UITestRunCase testCase)
        {
            stored.UpdateState(testCase);
            await _repository.SaveChangesAsync();
        }
    }
}
