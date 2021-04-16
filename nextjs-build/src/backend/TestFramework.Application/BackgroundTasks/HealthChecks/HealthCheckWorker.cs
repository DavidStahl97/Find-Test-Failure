using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks.HealthChecks;
using TestFramework.Application.Repository;

namespace TestFramework.Application.BackgroundTasks
{
    public class HealthCheckWorker : IIntervalBackgroundWorker
    {
        private readonly IHealthCheckClient _healthCheckClient;
        private readonly IRepository _repository;

        public HealthCheckWorker(IHealthCheckClient healthCheckClient, IRepository repository)
        {
            _healthCheckClient = healthCheckClient;
            _repository = repository;
        }

        public async Task ExecuteAsync()
        {
            var apis = await _repository.HealthChecks.GetAllAsync();
            foreach (var api in apis)
            {
                api.Healthy = await _healthCheckClient.HealthCheck(api.Url);
            }

            await _repository.SaveChangesAsync();
        }
    }
}
