using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.IntegrationTests
{
    public class IntegrationTestFixture
    {
        private readonly IServiceProvider _serviceProvider;

        public IntegrationTestFixture() 
        {
            Configuration = ReadConfiguration();
            _serviceProvider = BuildDependencyInjection();
        }

        public IntegrationTestsConfiguration Configuration { get; }

        public IHttpClientFactory HttpClientFactory => _serviceProvider.GetRequiredService<IHttpClientFactory>();

        private static IServiceProvider BuildDependencyInjection()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            return services.BuildServiceProvider();
        }

        private static IntegrationTestsConfiguration ReadConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var conf = new IntegrationTestsConfiguration();
            configuration.GetSection("Configuration").Bind(conf);
            return conf;
        }

    }
}
