using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Pipeline
{
    public interface IFunction<TRequest, TResponse, TFail> : IFunctionBase<TRequest, TResponse, TFail>
    {
        Response<TResponse, TFail> Execute(TRequest request);
    }
}
