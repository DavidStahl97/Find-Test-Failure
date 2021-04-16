using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Infrastructure.HttpRequest;
using TestFramework.Infrastructure.UITests.Dto;

namespace TestFramework.Infrastructure.UITests
{
    public class RemoteWebDriverApi : IRemoteWebDriverApi
    {
        private readonly IHttpService _httpService;
        private readonly ILogger<RemoteWebDriverApi> _logger;
        private readonly AsyncPolicyWrap<HttpRequestResponse<LogsResponse>> _retryPolicy;

        private readonly RemoteWebDriverApiOptions _options;

        public RemoteWebDriverApi(RemoteWebDriverApiOptions options, ILogger<RemoteWebDriverApi> logger, IHttpService httpService)
        {
            _options = options;
            _logger = logger;

            var handle = Policy<HttpRequestResponse<LogsResponse>>
                .Handle<HttpRequestException>();

            var retry = handle
                .WaitAndRetryAsync(options.MaxRetries, times => TimeSpan.FromMilliseconds(_options.WaitInMilliseconds),
                    onRetry: (result, duration) =>
                    {
                        _logger.WriteError(result.Exception, "Retry: Send to Remote Driver failed",
                            ("duration", duration));
                    });

            var fallback = handle
                .FallbackAsync(new HttpRequestResponse<LogsResponse>());

            _retryPolicy = fallback.WrapAsync(retry);
            _httpService = httpService;
        }

        public async Task<IEnumerable<EventLog>> GetLogsAsync(string sessionId)
        {
            var uri = new Uri($"{_options.Uri}session/{sessionId}/se/log");
            var requestBody = new LogRequest { Type = "browser" };

            var response = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _httpService.PostAsync<LogRequest, LogsResponse>(uri, requestBody);
            });

            if (response.WasSuccessful == false)
            {
                _logger.WriteFatal("Could not retrieve logs",
                    (nameof(sessionId), sessionId),
                    ("uri", uri),
                    ("HttpStatusCode", response.StatusCode));
                return new List<EventLog>();
            }

            return Convert(response.Resource.Value);
        }

        private static IEnumerable<EventLog> Convert(IEnumerable<Log> logs)
            => logs.Select(x => new EventLog
            {
                Message = x.Message,
                Timestamp = x.Timestamp,
                LogLevel = ConvertLogLevel(x.Level)
            });

        private static EventLogLevel ConvertLogLevel(string level)
            => level switch
            {
                "SEVERE" => EventLogLevel.Error,
                "WARNING" => EventLogLevel.Warning,
                _ => EventLogLevel.Unnone
            };
    }
}
