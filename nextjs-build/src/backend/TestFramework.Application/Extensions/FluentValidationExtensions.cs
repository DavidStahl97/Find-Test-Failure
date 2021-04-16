using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Types;

namespace TestFramework.Application.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, 
            ErrorCode errorCode)
        {
            rule.WithErrorCode(errorCode.ToString());
        }
    }
}
