using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Requests.UITests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UIElements
{
    public class GetPageHandler : AbstractHandler<PaginationRequest, PaginationResult<GetUIElementDto>, ErrorCodes>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPageHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<PaginationResult<GetUIElementDto>, ErrorCodes>> ExecuteAsync(PaginationRequest request)
        {
            var (page, count) = await _repository.UIElements.FindPageAsync(request.PageIndex, request.PageSize);
            var data = _mapper.Map<IEnumerable<GetUIElementDto>>(page);

            return new PaginationResult<GetUIElementDto>
            {
                Count = count,
                Data = data
            };
        }
    }
}
