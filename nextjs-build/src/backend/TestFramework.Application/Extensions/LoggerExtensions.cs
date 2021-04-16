using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application
{
    public static class LoggerExtensions
    {
        private static string[] _invalidPropertyNames = new string[] 
        { 
            "StatusCode"
        };

        public static void WriteInformation<T>(this ILogger<T> logger, string title, 
            params (string Name, object Value)[] properties)
            => LogMessage((message, values) => logger.LogInformation(message, values), 
                title, properties);

        public static void WriteError<T>(this ILogger<T> logger, Exception ex, string title,
            params (string Name, object Value)[] properties)
            => LogMessage((message, values) => logger.LogError(ex, message, values), title, properties);

        public static void WriteError<T>(this ILogger<T> logger, string title,
            params (string Name, object Value)[] properties)
            => LogMessage((message, values) => 
            { 
                logger.LogError(message, values); 
            }, title, properties);

        public static void WriteFatal<T>(this ILogger<T> logger, string title,
            params (string Name, object Value)[] properties)
            => LogMessage((message, values) => logger.LogCritical(message, values), title, properties);

        public static void WriteFatal<T>(this ILogger<T> logger, Exception ex, string title,
            params (string Name, object Value)[] properties)
            => LogMessage((message, value) => logger.LogCritical(ex, message, value), title, properties);

        private static void LogMessage(Action<string, object[]> logFunc,
            string title,
            params (string Name, object Value)[] properties)
        {
            if (properties.Any(x => _invalidPropertyNames.Contains(x.Name)))
            {
                throw new ArgumentException("Invalid Property Name for logging");
            }

            string message = title;

            foreach (var (Name, Value) in properties)
            {
                message += " \n " + Name + ": {" + Name + "}";
            }

            var values = properties.Select(x => x.Value.ToString() as object).ToArray();
            logFunc(message, values);
        }
    }
}
