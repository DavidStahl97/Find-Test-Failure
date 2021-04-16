using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Repository;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;
using TestFramework.Domain;

namespace TestFramework.Application.Handler.UserFiles
{
    public class PostHandler : AbstractHandler<StoreFileRequest, Success, ErrorCodes>
    {
        private readonly IRepository _repository;
        private readonly IFileStorage _fileStorage;

        public PostHandler(IRepository repository, IFileStorage fileStorage)
        {
            _repository = repository;
            _fileStorage = fileStorage;
        }

        public override async Task<Response<Success, ErrorCodes>> ExecuteAsync(StoreFileRequest fileRequest)
        {
            var fileFormat = fileRequest.FileName.Split('.').Last();
            var generatedFileName = Guid.NewGuid().ToString() + "." + fileFormat;

            await _fileStorage.Store(generatedFileName, fileRequest.Data);

            var userFile = new UserFile
            {
                FileName = fileRequest.FileName,
                FileSize = fileRequest.FileSize,
                StoredFileName = generatedFileName,
                CreatedDateTime = DateTime.UtcNow
            };

            _repository.UserFiles.Add(userFile);
            await _repository.SaveChangesAsync();

            return new Success();
        }
    }
}
