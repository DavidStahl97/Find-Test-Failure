using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;

namespace TestFramework.Application.Handler
{
    public abstract class AbstractHandler<TRequest, TResponse, TFail> : 
        IAsyncFunction<TRequest, TResponse, TFail>
    {
        public abstract Task<Response<TResponse, TFail>> ExecuteAsync(TRequest request);

        protected Task<Response<TResponse, TFail>> ReturnFailureAsync(TFail fail)
            => Task.FromResult(new Response<TResponse, TFail>(fail));

        protected Response<TResponse, TFail> ReturnFailure(TFail fail)
            => fail;

        protected Response<TResponse, TFail> ReturnResponse(TResponse response)
            => response;

        protected Task<Response<TResponse, TFail>> ReturnResponseAsync(TResponse response)
            => Task.FromResult(new Response<TResponse, TFail>(response));
    }
}
