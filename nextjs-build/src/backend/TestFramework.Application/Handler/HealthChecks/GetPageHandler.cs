using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.HealthChecks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.HealthChecks
{
    public class GetPageHandler : AbstractHandler<PaginationRequest, PaginationResult<GetHealthCheckDto>, ErrorCodes>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetPageHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<PaginationResult<GetHealthCheckDto>, ErrorCodes>> ExecuteAsync(PaginationRequest request)
        {
            var (page, count) = await _repository.HealthChecks.FindPageAsync(request.PageIndex, 
                request.PageSize, request.Search);

            var dto = _mapper.Map<IEnumerable<GetHealthCheckDto>>(page);
            return new PaginationResult<GetHealthCheckDto>
            {
                Count = count,
                Data = dto
            };
        }
    }
}
