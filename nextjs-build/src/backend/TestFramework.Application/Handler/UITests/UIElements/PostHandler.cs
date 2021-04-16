using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UIElements
{
    public class PostHandler : AbstractHandler<PostRequest, Created, ErrorCodes>
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PostHandler(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<Response<Created, ErrorCodes>> ExecuteAsync(PostRequest request)
        {
            var uiElement = _mapper.Map<UIElement>(request.Dto);
            _repository.UIElements.Add(uiElement);
            await _repository.SaveChangesAsync();

            return new Created { Id = uiElement.Id };
        }
    }
}
