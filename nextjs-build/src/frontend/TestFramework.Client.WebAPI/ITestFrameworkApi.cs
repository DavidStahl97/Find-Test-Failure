using System;
using System.Threading.Tasks;

namespace TestFramework.Client.WebAPI
{
    public interface ITestFrameworkApi
    {
        Task<T> SendAsync<T>(Func<swaggerClient, Task<T>> func,
            params (ErrorCodes ErrorCode, Func<T> Handle)[] handlers);

        Task<bool> SendAsync(Func<swaggerClient, Task> func,
            params (ErrorCodes ErrorCode, Action Handle)[] handlers);
    }
}