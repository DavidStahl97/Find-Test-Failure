using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UIPages
{
    public class PutHandler : AbstractHandler<PutRequest, Success, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;

        public PutHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<Success, Failure<NotFound, ErrorCodes>>> ExecuteAsync(PutRequest request)
        {
            var result = await _repository.UIPages.GetByIdAsync(request.Id);
            return await result.Match(
                page => UpdateAsync(request.Dto, page),
                notFound => ReturnFailureAsync(notFound));
        }

        private async Task<Response<Success, Failure<NotFound, ErrorCodes>>> UpdateAsync(ChangeOrCreateUIPageDto dto, Page page)
        {
            page.Name = dto.Name;
            await _repository.SaveChangesAsync();

            return new Success();
        }
    }
}
