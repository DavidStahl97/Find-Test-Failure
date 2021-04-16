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
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class UpdateEventRequest : INotification
    {
        public UITestRunEvent Event { get; init; }
    }

    public class UpdateTestEventHandler : INotificationHandler<UpdateEventRequest>
    {
        private readonly ILogger<UpdateTestEventHandler> _logger;
        private readonly IRepository _repository;

        public UpdateTestEventHandler(ILogger<UpdateTestEventHandler> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(UpdateEventRequest notification, CancellationToken cancellationToken)
        {
            var result = await _repository.UITestRunEvents.GetByIdAsync(notification.Event.Id);
            await result.Match(
                storedEvent =>
                {
                    storedEvent.UpdateState(notification.Event);
                    return _repository.SaveChangesAsync();
                },
                notFound =>
                {
                    _logger.LogError("Event {0} was not found", notification.Event.Id);
                    return Task.CompletedTask;
                });
        }
    }
}
