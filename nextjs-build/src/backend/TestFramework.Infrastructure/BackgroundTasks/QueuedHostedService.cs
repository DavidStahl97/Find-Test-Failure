using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.BackgroundTasks;

namespace TestFramework.Infrastructure.BackgroundTasks
{
    public class QueuedHostedService<TMessage> : BackgroundService
    {
        private readonly IBackgroundTaskQueue<TMessage> _queue;
        private readonly IEnumerable<WorkerTask<TMessage>> _workerTasks;
        private readonly ILogger<QueuedHostedService<TMessage>> _logger;

        public QueuedHostedService(IBackgroundTaskQueue<TMessage> taskQueue,
            ILogger<QueuedHostedService<TMessage>> logger, IEnumerable<IBackgroundWorker<TMessage>> worker)
        {
            _queue = taskQueue;
            _logger = logger;

            _workerTasks = worker.Select(x => new WorkerTask<TMessage>(x)).ToList();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"Queued Hosted Service is running.{Environment.NewLine}" +
                $"background queue.{Environment.NewLine}");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                (var message, var token) = await _queue.DequeueAsync(stoppingToken);

                var freeWorker = await GetFreeWorker();
                freeWorker.Execute(message, token);
            }
        }

        private async Task<WorkerTask<TMessage>> GetFreeWorker()
        {
            var tasks = _workerTasks.Select(x => x.Task).ToList();
            await Task.WhenAny(tasks);
            var freeWorker = _workerTasks.First(x => x.IsBusy == false);

            if (freeWorker.Task.IsFaulted)
            {
                _logger.LogCritical(freeWorker.Task.Exception, "executing Background task fails");
            }

            return freeWorker;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
