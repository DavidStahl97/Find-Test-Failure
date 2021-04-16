using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UITestCases
{
    public class GetPageHandler : AbstractHandler<PaginationRequest, PaginationResult<GetUITestCaseDto>, ErrorCodes>    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPageHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<PaginationResult<GetUITestCaseDto>, ErrorCodes>> ExecuteAsync(
            PaginationRequest request)
        {
            (var page, int count) = await _repository.UITestCases.FindPageAsync(request.PageIndex, 
                request.PageSize, request.Search);
            var dto = _mapper.Map<IEnumerable<GetUITestCaseDto>>(page);
            return new PaginationResult<GetUITestCaseDto>
            {
                Count = count,
                Data = dto
            };
        }
    }
}
