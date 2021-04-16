using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.BackgroundTasks.HealthChecks;
using TestFramework.Application.BackgroundTasks.TestErrorNotification;
using TestFramework.Application.BackgroundTasks.UITests;
using TestFramework.Application.Repository;
using TestFramework.Application.UITests;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Infrastructure.BackgroundTasks;
using TestFramework.Infrastructure.FileStorage;
using TestFramework.Infrastructure.HealthChecks;
using TestFramework.Infrastructure.MicrosoftTeams;
using TestFramework.Infrastructure.Repository;
using TestFramework.Infrastructure.Repository.Database;
using TestFramework.Infrastructure.UITests;

namespace TestFramework.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IJsonSerialization, JsonSerialization>();
            services.AddTransient<IHttpService, HttpService>();

            services.AddRepository(configuration);

            services.AddNotifyMicrosoftTeams(configuration);

            int uiTestInterval = int.Parse(configuration["RunUITestsIntervalInMinutes"]);
            services.AddIntervalBackgroundTask<RunTestsPeriodicallyWorker>(new IntervalHostedServiceOptions
            {
                Interval = TimeSpan.FromMinutes(uiTestInterval)
            });

            services.AddSingleton<IHealthCheckClient, HealthCheckClient>();
            services.AddIntervalBackgroundTask<HealthCheckWorker>(new IntervalHostedServiceOptions
            {
                Interval = TimeSpan.FromSeconds(5)
            });

            services.AddUITestingInBackground(configuration);

            services.AddFileStorage(configuration);
        }

        private static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRepository, Repository.Repository>();

            services.AddDbContext<RepositoryContext>(options =>
                DatabaseStartup.ConfigureDatabaseOptions(options, configuration),
                optionsLifetime: ServiceLifetime.Singleton,
                contextLifetime: ServiceLifetime.Transient);
                        services.AddDatabaseDeveloperPageExceptionFilter();
        }

        private static void AddIntervalBackgroundTask<TWorker>(this IServiceCollection services, 
            IntervalHostedServiceOptions options)
            where TWorker : class, IIntervalBackgroundWorker
        {
            services.AddTransient<TWorker>();
            services.AddHostedService(provider => 
            {
                var logger = provider.GetRequiredService<ILogger<IntervalHostedService<TWorker>>>();
                var worker = provider.GetRequiredService<TWorker>();
                return new IntervalHostedService<TWorker>(logger, options, provider);
            });
        }

        private static void AddUITestingInBackground(this IServiceCollection services,
            IConfiguration configuration)
        {
            var uiTestingOptions = new WebUITestingOptions();
            configuration.GetSection(WebUITestingOptions.Position).Bind(uiTestingOptions);

            var remoteWebDriverApiOptions = new RemoteWebDriverApiOptions();
            configuration.GetSection(RemoteWebDriverApiOptions.Position).Bind(remoteWebDriverApiOptions);

            services.AddSingleton(remoteWebDriverApiOptions);
            services.AddTransient<IRemoteWebDriverApi, RemoteWebDriverApi>();
            services.AddSingleton(uiTestingOptions);
            services.AddBackgroundQueueService<UITestRunCase, UITestWorker>(uiTestingOptions.NumberOfWorkers);
            services.AddTransient<IWebUITestFactory, WebUITestFactory>();
        }

        private static void AddBackgroundQueueService<TMessage, TWorker>(this IServiceCollection services, int numberOfWorker)
            where TWorker : class, IBackgroundWorker<TMessage>
        {
            services.AddHostedService<QueuedHostedService<TMessage>>();
            services.AddSingleton<IBackgroundTaskQueue<TMessage>, BackgroundTaskQueue<TMessage>>();
            services.AddTransient<IBackgroundWorker<TMessage>, TWorker>();
            services.AddSingleton<IEnumerable<IBackgroundWorker<TMessage>>>(sp =>
                Enumerable.Range(0, numberOfWorker)
                    .Select(i => sp.GetRequiredService<IBackgroundWorker<TMessage>>())
                    .ToList());
        }

        private static void AddFileStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new FileStorageOptions();
            configuration.GetSection(FileStorageOptions.Position).Bind(options);

            services.AddSingleton(options);
            services.AddSingleton<IFileStorage, FileStorage.FileStorage>();
        }

        private static void AddNotifyMicrosoftTeams(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new MicrosoftTeamsApiOptions();
            configuration.GetSection(MicrosoftTeamsApiOptions.Position).Bind(options);

            services.AddSingleton(options);
            services.AddSingleton<IMicrosoftTeamsApi, MicrosoftTeamsApi>();
            services.AddSingleton<ITestErrorNotifyService, TestErrorNotifyService>();

            services.AddIntervalBackgroundTask<TestErrorNotificationWorker>(new IntervalHostedServiceOptions
            {
                Interval = TimeSpan.FromSeconds(5)
            });
        }
    }
}
