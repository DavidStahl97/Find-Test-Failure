using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application;

namespace TestFramework.Infrastructure.BackgroundTasks
{
    public class IntervalHostedService<TWorker> : BackgroundService
        where TWorker : IIntervalBackgroundWorker
    {
        private readonly ILogger<IntervalHostedService<TWorker>> _logger;
        private readonly IntervalHostedServiceOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public IntervalHostedService(ILogger<IntervalHostedService<TWorker>> logger, 
            IntervalHostedServiceOptions options, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var worker = _serviceProvider.GetRequiredService<TWorker>();
                    await worker.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "executing Background task fails");
                }

                await Task.Delay(_options.Interval, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
