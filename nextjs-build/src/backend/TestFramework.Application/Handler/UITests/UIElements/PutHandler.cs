using AutoMapper;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UIElements
{
    public class PutHandler : AbstractHandler<PutRequest, Success, Failure<ErrorCodes, NotFound>>
    {
        private readonly IRepository _repository;

        public PutHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<Success, Failure<ErrorCodes, NotFound>>> ExecuteAsync(PutRequest request)
        {
            var result = await _repository.UIElements.GetByIdAsync(request.Id);
            return await result.Match(
                uiElement => UpdateAsync(uiElement, request.Dto),
                notFound => ReturnFailureAsync(notFound));
        }

        private async Task<Response<Success, Failure<ErrorCodes, NotFound>>> UpdateAsync(UIElement uiElement, ChangeOrCreateUIElemenDto dto)
        {
            uiElement.Name = dto.Name;
            uiElement.FindBy = dto.FindBy;
            uiElement.FindByMethod = dto.FindByMethod;
            await _repository.SaveChangesAsync();

            return new Success();
        }
    }
}
