using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks.HealthChecks;

namespace TestFramework.Infrastructure.HealthChecks
{
    public class HealthCheckClient : IHealthCheckClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<HealthCheckClient> _logger;

        public HealthCheckClient(IHttpClientFactory factory, ILogger<HealthCheckClient> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        public async Task<bool> HealthCheck(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var client = _factory.CreateClient();
            
            try
            {
                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "health check failure");
                return false;
            }
        }
    }
}
