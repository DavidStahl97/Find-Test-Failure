using AutoMapper;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler.UITests.UIElements
{
    public class GetByIdHandler : AbstractHandler<int, GetUIElementDto, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetByIdHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<GetUIElementDto, Failure<NotFound, ErrorCodes>>> ExecuteAsync(int id)
        {
            var uiElement = await _repository.UIElements.GetByIdAsync(id);
            return uiElement.Match(
                uiElement => _mapper.Map<GetUIElementDto>(uiElement),
                notFound => ReturnFailure(notFound));
        }
    }
}
