using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Polly.Fallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestFramework.Infrastructure.MicrosoftTeams.Dto;
using Polly.Wrap;
using TestFramework.Application;
using OneOf.Types;
using TestFramework.Infrastructure.HttpRequest;

namespace TestFramework.Infrastructure.MicrosoftTeams
{
    public class MicrosoftTeamsApi : IMicrosoftTeamsApi
    {
        private readonly AsyncPolicyWrap<HttpRequestResponse> _retryPolicy;

        private readonly ILogger<MicrosoftTeamsApi> _logger;
        private readonly MicrosoftTeamsApiOptions _options;

        private readonly IHttpService _httpService;

        public MicrosoftTeamsApi(MicrosoftTeamsApiOptions options, ILogger<MicrosoftTeamsApi> logger, IHttpService httpService)
        {
            _options = options;
            _logger = logger;
            _httpService = httpService;


            var handle = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpRequestResponse>(message => message.StatusCode == HttpStatusCode.TooManyRequests);

            var retry = handle
                .WaitAndRetryAsync(options.MaxRetries, times => TimeSpan.FromMilliseconds(_options.WaitInMilliseconds),
                    onRetry: (response, duration) =>
                    {
                        if (response.Exception is null)
                        {
                            _logger.WriteError("Send Card to Microsoft Teams failed",
                                ("HttpStatusCode", response.Result.StatusCode),
                                ("duration", duration));
                        }
                        else
                        {
                            _logger.WriteError(response.Exception, "Send Card to Microsoft Teams failed",
                                ("duration", duration));
                        }
                    });

            var fallback = handle
                .FallbackAsync(new HttpRequestResponse());

            _retryPolicy = fallback.WrapAsync(retry);
        }

        public async Task<TrueOrFalse> PostMessage(Card card)
        {
            var response = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _httpService.PostAsync(_options.Uri, card);
            });

            return response.WasSuccessful;
        }
    }
}
