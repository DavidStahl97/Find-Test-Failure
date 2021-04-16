using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Types;

namespace TestFramework.Application.Validation
{
    public abstract class Validator<TRequest, TFail> : AbstractValidator<TRequest>,
            IFunction<TRequest, TRequest, TFail>
    {
        public Response<TRequest, TFail> Execute(TRequest request)
        {
            var errorCodeStrings = Validate(request).Errors
                .Where(x => x != null)
                .Select(x => x.ErrorCode)
                .ToList();

            if (errorCodeStrings.Any())
            {
                var errorCodes = errorCodeStrings
                    .Select(x => Enum.Parse<ErrorCode>(x))
                    .ToList();

                return CreateErrors(new ErrorCodes(errorCodes));
            }

            return request;
        }

        protected abstract TFail CreateErrors(ErrorCodes errorCodes);
    }
}
