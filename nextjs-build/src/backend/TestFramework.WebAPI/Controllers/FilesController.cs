using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.Dtos;
using TestFramework.Application.Handler.UserFiles;
using TestFramework.Application.Pipeline;
using TestFramework.Application.Requests;
using TestFramework.Application.Validation;
using TestFramework.Application.Validation.UserFiles;
using TestFramework.WebAPI.Attributes;
using TestFramework.WebAPI.Extensions;

namespace TestFramework.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IPipelineBuilder _builder;

        public FilesController(IPipelineBuilder builder)
        {
            _builder = builder;
        }

        [HttpGet(Name = nameof(GetUserFilePage))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<ActionResult<PaginationResult<GetUserFileDto>>> GetUserFilePage(
            int pageIndex, int pageSize, string search, [FromServices] GetPageHandler handler)
        {
            var request = new PaginationRequest
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Search = search
            };

            var response = await _builder.BuildPipeline(new PaginationValidation(), handler)(request);

            return response.Match(this.ReturnOk, this.ReturnErrorCode<PaginationResult<GetUserFileDto>>);
        }

        [HttpPost(Name = nameof(PostUserFile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostUserFile([FromForm] IEnumerable<IFormFile> files, 
            [FromServices] PostHandler handler)
        {
            foreach (var file in files)
            {
                await StoreFile(file, handler);
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = nameof(DeleteUserFile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [UnprocessableEntityResponseType]
        public async Task<IActionResult> DeleteUserFile(int id, [FromServices] DeleteHandler handler)
        {
            var response = await _builder.BuildPipeline(handler)(id);
            return response.Match(this.ReturnOk,
                failure => failure.Match(this.ReturnNotFound, this.ReturnErrorCode));
        }

        private async Task StoreFile(IFormFile file, PostHandler handler)
        {            
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var data = memoryStream.ToArray();

            var request = new StoreFileRequest
            {
                FileName = file.FileName,
                FileSize = file.Length,
                Data = data
            };

            await _builder.BuildPipeline(new PostValidation(), handler)(request);
        }
    }
}
