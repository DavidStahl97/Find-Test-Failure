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
    public class UpdateStartTestCaseRequest : INotification
    {
        public UITestRunCase TestCase { get; init; }
    }

    public class UpdateStartTestCaseHandler : INotificationHandler<UpdateStartTestCaseRequest>
    {
        private readonly ILogger<UpdateTestEventHandler> _logger;
        private readonly IRepository _repository;

        public UpdateStartTestCaseHandler(ILogger<UpdateTestEventHandler> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(UpdateStartTestCaseRequest notification, CancellationToken cancellationToken)
        {
            var updatedTestCase = notification.TestCase;

            var result = await _repository.UITestRunCases.GetByIdAsync(updatedTestCase.Id);
            await result.Match(
                stored => UpdateAsync(stored, updatedTestCase),
                notFound =>
                {
                    _logger.LogCritical("Run Test Case not found with id {0}", notification.TestCase.Id);
                    return Task.CompletedTask;
                });
        }

        private async Task UpdateAsync(UITestRunCase stored, UITestRunCase updated)
        {
            stored.Start = updated.Start;
            stored.State = updated.State;

            await _repository.SaveChangesAsync();
        }
    }
}
