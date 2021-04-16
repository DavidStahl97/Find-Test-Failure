using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Pipeline
{
    public class PipelineBuilder : IPipelineBuilder
    {
        public Func<T1, Task<Response<T3, TFail>>> BuildPipeline<T1, T2, T3, TFail>(
            IFunctionBase<T1, T2, TFail> func1,
            IFunctionBase<T2, T3, TFail> func2)
        {
            return async (T1 request) =>
            {
                var response = await ExecuteAsync(func1, request);
                return await response.Match(
                    requestTwo => ExecuteAsync(func2, requestTwo),
                    fail => Task.FromResult(new Response<T3, TFail>(fail)));
            };
        }
        
        public Func<T1, Task<Response<T2, TFail>>> BuildPipeline<T1, T2, TFail>(IFunctionBase<T1, T2, TFail> func)
        {
            return async (T1 request) => await ExecuteAsync(func, request);
        }

        private static async Task<Response<T2, TFail>> ExecuteAsync<T1, T2, TFail>(
            IFunctionBase<T1, T2, TFail> func,
            T1 request)
        {
            if (func is IFunction<T1, T2, TFail> f)
            {
                return f.Execute(request);
            }
            else if (func is IAsyncFunction<T1, T2, TFail> asyncF)
            {
                return await asyncF.ExecuteAsync(request);
            }
            else
            {
                throw new Exception("No valid Function. Use IFunction or IAsyncFunction");
            }
        }
    }
}
