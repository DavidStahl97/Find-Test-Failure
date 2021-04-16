using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application;

namespace TestFramework.Infrastructure
{
    public class ParallelPublisher : Mediator, IParallelPublisher
    {
        private readonly ILogger<ParallelPublisher> _logger;

        public ParallelPublisher(ServiceFactory serviceFactory, ILogger<ParallelPublisher> logger) : base(serviceFactory)
        {
            _logger = logger;
        }

        protected override Task PublishCore(IEnumerable<Func<INotification, CancellationToken, Task>> allHandlers, INotification notification, CancellationToken cancellationToken)
        {
            var actions = allHandlers.Select(x => new Action(() => ExecuteHandlerLogic(x, notification, cancellationToken)))
                .ToArray();

            Parallel.Invoke(actions);

            return Task.CompletedTask;
        }

        private async void ExecuteHandlerLogic(Func<INotification, CancellationToken, Task> func, INotification notification,
            CancellationToken token)
        {
            try
            {
                await func(notification, token);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Handler exception");
            }
        }
    }
}
