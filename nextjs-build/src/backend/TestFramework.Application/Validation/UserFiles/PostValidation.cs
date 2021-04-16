using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Extensions;
using TestFramework.Application.Requests;
using TestFramework.Application.Types;
using TestFramework.Contract;

namespace TestFramework.Application.Validation.UserFiles
{
    public class PostValidation : Validator<StoreFileRequest, ErrorCodes>
    {
        public PostValidation()
        {
            RuleFor(x => x.FileName)
                .NotEmpty()
                .WithErrorCode(ErrorCode.UserFiles_FileNameIsEmpty);

            RuleFor(x => x.FileName)
                .MaximumLength(Contracts.UserFiles.FileNameMaxLength)
                .WithErrorCode(ErrorCode.UserFiles_FileNameMaxLength);

            RuleFor(x => x.FileSize)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode(ErrorCode.UserFiles_FileSizeIsNegative);            
        }

        protected override ErrorCodes CreateErrors(ErrorCodes errorCodes) => errorCodes;
    }
}
