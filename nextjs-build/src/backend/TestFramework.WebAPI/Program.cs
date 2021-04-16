using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.WebAPI
{
    public class Program
    {
        public static readonly string ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static void Main(string[] args)
        {
            // Needed for sending files to Selenium Grid
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Startups before running ASP.NET
            var configuration = BuildConfiguration();
            LogStartup.StartLogging(configuration);
            DatabaseStartup.Migrate(configuration);

            // Start ASP.NET
            CreateHost(args);
        }

        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, $"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}");
                Thread.Sleep(1000);
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static IConfiguration BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(
                    $"appsettings.{ENVIRONMENT}.json",
                    optional: true)
                .AddEnvironmentVariables()
                .Build();

            return configuration;
        }
    }
}
