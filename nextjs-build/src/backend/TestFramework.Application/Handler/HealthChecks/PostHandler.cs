using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Types;
using TestFramework.Domain;

namespace TestFramework.Application.Handler.HealthChecks
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
            var healthCheck = _mapper.Map<HealthCheck>(request.Dto);

            _repository.HealthChecks.Add(healthCheck);
            await _repository.SaveChangesAsync();

            return new Created { Id = healthCheck.Id };
        }
    }
}
