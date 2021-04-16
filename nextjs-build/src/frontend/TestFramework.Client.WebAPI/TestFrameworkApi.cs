using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Client.WebAPI
{
    public class TestFrameworkApi : ITestFrameworkApi
    {
        private readonly HttpClient _client;

        public TestFrameworkApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> SendAsync<T>(Func<swaggerClient, Task<T>> func,
            params (ErrorCodes ErrorCode, Func<T> Handle)[] handlers)
            => await TrySendAsync(async () => 
            {
                return await func(CreateClient());            
            }, handlers);

        public async Task<bool> SendAsync(Func<swaggerClient, Task> func,
            params (ErrorCodes ErrorCode, Action Handle)[] handlers)
            => await TrySendAsync(async () =>
            {
                await func(CreateClient());
                return true;
            }, handlers.Select(x => 
            {
                bool action()
                {
                    x.Handle();
                    return false;
                }
                return (x.ErrorCode, (Func<bool>)action);
            }).ToArray());

        private swaggerClient CreateClient() => new(string.Empty, _client);

        private static async Task<T> TrySendAsync<T>(Func<Task<T>> sendFunc,
            params (ErrorCodes ErrorCode, Func<T> Handle)[] handlers)
        {
            try
            {
                return await sendFunc();
            }
            catch (ApiException<ErrorCodesResult> exception)
            {
                var (errorCode, handle) = handlers.FirstOrDefault(x => 
                    exception.Result.ErrorCodes.Any(code => code == x.ErrorCode));
                if (handle is null)
                {
                    throw;
                }

                return handle();
            }
        }
    }
}
