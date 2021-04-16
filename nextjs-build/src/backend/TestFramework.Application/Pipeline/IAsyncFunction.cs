using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Pipeline
{
    public interface IAsyncFunction<TRequest, TResponse, TFail> : IFunctionBase<TRequest, TResponse, TFail>
    {
        Task<Response<TResponse, TFail>> ExecuteAsync(TRequest request);
    }
}
