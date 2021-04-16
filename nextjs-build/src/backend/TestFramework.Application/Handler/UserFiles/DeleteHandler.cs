using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Types;
using TestFramework.Domain;

namespace TestFramework.Application.Handler.UserFiles
{
    public class DeleteHandler : AbstractHandler<int, Success, Failure<NotFound, ErrorCodes>>
    {
        private readonly IRepository _repository;
        private readonly IFileStorage _fileStorage;

        public DeleteHandler(IRepository repository, IFileStorage fileStorage)
        {
            _repository = repository;
            _fileStorage = fileStorage;
        }

        public override async Task<Response<Success, Failure<NotFound, ErrorCodes>>> ExecuteAsync(int id)
        {
            var result = await _repository.UserFiles.GetByIdAsync(id);
            return await result.Match(
                userFile => DeleteUserFile(userFile),
                notFound => ReturnFailureAsync(new NotFound()));
        }

        private async Task<Response<Success, Failure<NotFound, ErrorCodes>>> DeleteUserFile(UserFile userFile)
        {
            var result = await _repository.TryDeleteEntityAsync(userFile);
            return result.Match(
                t => 
                {
                    _fileStorage.Delete(userFile.StoredFileName);
                    return new Success();
                },
                f => ReturnFailure(new ErrorCodes(ErrorCode.DeletionFailed)));
        }
    }
}
