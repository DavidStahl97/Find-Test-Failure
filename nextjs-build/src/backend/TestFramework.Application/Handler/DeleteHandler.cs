using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;

namespace TestFramework.Application.Handler
{
    public class DeleteHandler<TEntity> : AbstractHandler<TEntity, Success, ErrorCodes>
    {
        private readonly IRepository _repository;

        public DeleteHandler(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task<Response<Success, ErrorCodes>> ExecuteAsync(TEntity entity)
        {
            var result = await _repository.TryDeleteEntityAsync(entity);
            return result.Match(
                success => new Success(),
                failure => ReturnFailure(new ErrorCodes(ErrorCode.DeletionFailed)));
        }
    }
}
