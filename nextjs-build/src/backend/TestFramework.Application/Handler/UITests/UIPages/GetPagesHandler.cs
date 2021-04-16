using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UIPages
{
    public class GetPagesHandler : AbstractHandler<PaginationRequest, PaginationResult<GetUIPageDto>, ErrorCodes>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPagesHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<PaginationResult<GetUIPageDto>, ErrorCodes>> ExecuteAsync(PaginationRequest request)
        {
            var (page, count) = await _repository.UIPages.FindPageAsync(request.PageIndex, request.PageSize, 
                request.Search);
            var dto = _mapper.Map<IEnumerable<GetUIPageDto>>(page);
            return new PaginationResult<GetUIPageDto>
            {
                Data = dto,
                Count = count,
            };
        }
    }
}
