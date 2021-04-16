using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UITestCases
{
    public class PutHandler : AbstractHandler<PutRequest, Success, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;

        public PutHandler(IRepository repository)
        {
            this._repository = repository;
        }

        public override async Task<Response<Success, Failure<NotFound, ErrorCodes>>> ExecuteAsync(PutRequest request)
        {
            var result = await _repository.UITestCases.GetByIdAsync(request.Id);
            return await result.Match(
                stored => UpdateAsync(stored, request.Dto),
                notFound => ReturnFailureAsync(notFound));
        }

        private async Task<Response<Success, Failure<NotFound, ErrorCodes>>> UpdateAsync(UITestCase stored, 
            ChangeOrCreateUITestCaseDto updated)
        {
            stored.Name = updated.Name;
            stored.StartUrl = updated.StartUrl;
            stored.DefaultWaitForUIElement = updated.DefaultWaitForUIElement;
            stored.RunsPeriodically = updated.RunsPeriodically;
            await _repository.SaveChangesAsync();

            return new Success();
        }
    }
}
