using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Infrastructure.HttpRequest;

namespace TestFramework.Infrastructure
{
    public class HttpService : IHttpService
    {
        private readonly ILogger<HttpService> _logger;
        private readonly IHttpClientFactory _httpFactory;
        private readonly IJsonSerialization _jsonSerialization;

        public HttpService(IHttpClientFactory httpFactory, IJsonSerialization jsonSerialization, ILogger<HttpService> logger)
        {
            _httpFactory = httpFactory;
            _jsonSerialization = jsonSerialization;
            _logger = logger;
        }

        public async Task<HttpRequestResponse> PostAsync<T>(Uri url, T resource)
        {
            var client = _httpFactory.CreateClient();
            var body = Deserialize(resource);
            var resposneMessage = await client.PostAsync(url, body);
            return await HandleResponse(resposneMessage);
        }

        public async Task<HttpRequestResponse<TResponse>> PostAsync<TRequest, TResponse>(Uri uri, TRequest requestBody)
        {
            var client = _httpFactory.CreateClient();
            var body = Deserialize(requestBody);
            var responseMessage = await client.PostAsync(uri, body);
            return await HandleResponseWithBody<TResponse>(responseMessage);
        }

        private HttpContent Deserialize<T>(T resource)
        {
            var json = _jsonSerialization.Serialize(resource);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }

        private async Task<HttpRequestResponse> HandleResponse(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode == false)
            {
                await LogResponseMessage(responseMessage);
            }

            return new HttpRequestResponse
            {
                WasSuccessful = responseMessage.IsSuccessStatusCode,
                StatusCode = responseMessage.StatusCode
            };
        }

        private async Task<HttpRequestResponse<T>> HandleResponseWithBody<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode == false)
            {
                await LogResponseMessage(responseMessage);
                return new HttpRequestResponse<T>
                {
                    WasSuccessful = responseMessage.IsSuccessStatusCode,
                    StatusCode = responseMessage.StatusCode
                };
            }

            var responseBody = await responseMessage.Content.ReadAsStringAsync();
            var resource = _jsonSerialization.Deserialize<T>(responseBody);
            return new HttpRequestResponse<T>
            {
                WasSuccessful = responseMessage.IsSuccessStatusCode,
                StatusCode = responseMessage.StatusCode,
                Resource = resource
            };
        }

        private async Task LogResponseMessage(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var requestBody = await response.RequestMessage.Content.ReadAsStringAsync();

            _logger.WriteFatal("Http Request faile",
                ("HttpStatusCode", response.StatusCode),
                ("ResponseBody", responseBody),
                ("RequestBody", requestBody),
                (nameof(response.RequestMessage), response.RequestMessage));
        }
    }
}
