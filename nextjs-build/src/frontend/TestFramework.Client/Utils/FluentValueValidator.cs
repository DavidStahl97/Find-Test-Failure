using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Utils
{
    /// <summary>
    /// A glue class to make it easy to define validation rules for single values using FluentValidation
    /// You can reuse this class for all your fields, like for the credit card rules above.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FluentValueValidator<T> : AbstractValidator<ValueHolder<T>>
    {
        public FluentValueValidator(Action<IRuleBuilderInitial<ValueHolder<T>, T>> rule)
        {
            rule(RuleFor(x => x.Value));
        }

        private IEnumerable<string> ValidateValue(T arg)
        {
            var result = Validate(new ValueHolder<T> { Value = arg});
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        }

        public Func<T, IEnumerable<string>> Validation => ValidateValue;
    }

    public class ValueHolder<T>
    {
        public T Value { get; init; }
    }
}
