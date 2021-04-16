using System;
using System.Threading.Tasks;

namespace TestFramework.Application.Pipeline
{
    public interface IPipelineBuilder
    {
        Func<T1, Task<Response<T3, TFail>>> BuildPipeline<T1, T2, T3, TFail>(IFunctionBase<T1, T2, TFail> func1, IFunctionBase<T2, T3, TFail> func2);

        Func<T1, Task<Response<T2, TFail>>> BuildPipeline<T1, T2, TFail>(IFunctionBase<T1, T2, TFail> func);
    }
}