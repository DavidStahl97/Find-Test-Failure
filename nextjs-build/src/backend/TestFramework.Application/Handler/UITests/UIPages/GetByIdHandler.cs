using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;

namespace TestFramework.Application.Handler.UITests.UIPages
{
    public class GetByIdHandler : AbstractHandler<int, GetUIPageDto, NotFound>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<GetUIPageDto, NotFound>> ExecuteAsync(int request)
        {
            var response = await _repository.UIPages.GetByIdAsync(request);
            return response.Match(
                page => _mapper.Map<GetUIPageDto>(page),
                notFound => ReturnFailure(notFound));
        }
    }
}
