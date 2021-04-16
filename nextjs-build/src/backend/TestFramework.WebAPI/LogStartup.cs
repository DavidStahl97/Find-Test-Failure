using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestFramework.WebAPI
{
    public static class LogStartup
    {
        public static void StartLogging(IConfiguration configuration)
            => ConfigureLogging(configuration);

        private static void ConfigureLogging(IConfiguration configuration)
        {
            var builder = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, Program.ENVIRONMENT))
                .Enrich.WithProperty("Environment", Program.ENVIRONMENT)
                .ReadFrom.Configuration(configuration);

            if (Program.ENVIRONMENT == "Developement")
            {
                builder = builder.WriteTo.Debug();
            }

            Log.Logger = builder.CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                CustomFormatter = new ElasticsearchJsonFormatter()
            };
        }
    }
}
