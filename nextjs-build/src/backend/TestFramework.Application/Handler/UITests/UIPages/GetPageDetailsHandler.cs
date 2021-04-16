using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UIPages
{
    public class GetPageDetailsHandler : AbstractHandler<GetDetailsRequest, GetUIPageDetailsDto, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPageDetailsHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<GetUIPageDetailsDto, Failure<NotFound, ErrorCodes>>> ExecuteAsync(GetDetailsRequest request)
        {
            var result = await _repository.UIPages.FindUIElementPageAsync(request.Pagination.PageIndex, 
                request.Pagination.PageSize, request.Id, request.Pagination.Search);

            return result.Match(
                page => CreateDto(page.Page, page.Count),
                notFound => ReturnFailure(notFound));
        }

        private Response<GetUIPageDetailsDto, Failure<NotFound, ErrorCodes>> CreateDto(Page page, int count)
        {
            var dto = _mapper.Map<GetUIPageDetailsDto>(page);
            var uiElements = _mapper.Map<IEnumerable<GetUIElementDto>>(page.UIElements);
            
            dto.Page = new Dtos.PaginationResult<GetUIElementDto>
            {
                Count = count,
                Data = uiElements
            };

            return dto;
        }
    }
}
