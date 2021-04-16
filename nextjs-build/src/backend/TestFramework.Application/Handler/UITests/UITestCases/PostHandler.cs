using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.UITests.UITestCases;
using TestFramework.Application.Types;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Template;

namespace TestFramework.Application.Handler.UITests.UITestCases
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
            var testCase = _mapper.Map<UITestCase>(request.Dto);
            _repository.UITestCases.Add(testCase);
            await _repository.SaveChangesAsync();

            return new Created { Id = testCase.Id };
        }
    }
}
