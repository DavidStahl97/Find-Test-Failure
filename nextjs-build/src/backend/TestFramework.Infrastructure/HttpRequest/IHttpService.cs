using System;
using System.Threading.Tasks;
using TestFramework.Infrastructure.HttpRequest;

namespace TestFramework.Infrastructure
{
    public interface IHttpService
    {
        Task<HttpRequestResponse> PostAsync<T>(Uri url, T resource);

        Task<HttpRequestResponse<TResponse>> PostAsync<TRequest, TResponse>(Uri uri, TRequest requestBody);
    }
}