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
using TestFramework.Application.Requests.UITests.UIPages;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UIPages
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
            var page = _mapper.Map<Page>(request.Dto);
            _repository.UIPages.Add(page);
            await _repository.SaveChangesAsync();

            return new Created { Id = page.Id };
        }
    }
}
