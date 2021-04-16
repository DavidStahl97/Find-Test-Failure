using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Application.Handler.UITests.UITestRunCases
{
    public class GetStatusInfosHandler : AbstractHandler<GetStatusRequest, IEnumerable<TestCasesStatusInfo>, ErrorCodes>
    {
        private readonly IRepository _repository;

        public GetStatusInfosHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<IEnumerable<TestCasesStatusInfo>, ErrorCodes>> ExecuteAsync(GetStatusRequest request)
        {
            var result = await _repository.UITestRunCases.GetStatusInfos(request.Start, request.End);
            return ReturnResponse(result);
        }
    }
}
