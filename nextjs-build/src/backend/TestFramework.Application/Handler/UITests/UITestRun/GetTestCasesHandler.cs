using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class GetTestCasesHandler : AbstractHandler<int, GetUITestRunCasesDto, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetTestCasesHandler(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<Response<GetUITestRunCasesDto, Failure<NotFound, ErrorCodes>>> ExecuteAsync(int request)
        {
            var response = await _repository.UITestRuns.GetByIdWithTestCasesAsync(request);
            return response.Match(
                run => _mapper.Map<GetUITestRunCasesDto>(run),
                notFound => ReturnFailure(notFound));
        }
    }
}
