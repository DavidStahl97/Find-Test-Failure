using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.HealthChecks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests.HealthChecks;
using TestFramework.Application.Types;
using TestFramework.Domain;

namespace TestFramework.Application.Handler.HealthChecks
{
    public class PutHandler : AbstractHandler<PutRequest, Success, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;

        public PutHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<Success, Failure<NotFound, ErrorCodes>>> ExecuteAsync(PutRequest request)
        {
            var result = await _repository.HealthChecks.GetByIdAsync(request.Id);
            return await result.Match(
                healthCheck => UpdateAsync(healthCheck, request.Dto),
                notFound => ReturnFailureAsync(notFound));
        }

        private async Task<Response<Success, Failure<NotFound, ErrorCodes>>> UpdateAsync(HealthCheck healthCheck, 
            ChangeOrCreateHealthCheckDto update)
        {
            healthCheck.Name = update.Name;
            healthCheck.Url = update.Url;
            await _repository.SaveChangesAsync();

            return new Success();
        }
    }
}
