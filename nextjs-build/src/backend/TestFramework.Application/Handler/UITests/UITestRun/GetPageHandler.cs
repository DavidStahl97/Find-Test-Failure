using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class GetPageHandler : AbstractHandler<PaginationRequest, PaginationResult<GetUITestRunDto>, ErrorCodes>
    {
        private readonly IRepository _repository;

        public GetPageHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<PaginationResult<GetUITestRunDto>, ErrorCodes>> ExecuteAsync(
            PaginationRequest request)
        {
            (var page, var count) = await _repository.UITestRuns.FindPageAsync(request.PageIndex, request.PageSize);

            return new PaginationResult<GetUITestRunDto>
            {
                Count = count,
                Data = page
            };
        }
    }
}
