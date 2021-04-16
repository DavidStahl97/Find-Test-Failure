using Microsoft.AspNetCore.Mvc;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Application.Types;

namespace TestFramework.WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ReturnCreated(this ControllerBase controller, string url, Created created)
            => controller.Created(new Uri($"{url}{created.Id}", UriKind.Relative), new { created.Id });

        public static ActionResult<TDto> ReturnErrorCode<TDto>(this ControllerBase controller, ErrorCodes errorCodes)
            => controller.UnprocessableEntity(new ErrorCodesResult(errorCodes));

        public static IActionResult ReturnErrorCode(this ControllerBase controller, ErrorCodes errorCodes)
            => controller.UnprocessableEntity(new ErrorCodesResult(errorCodes));

        public static ActionResult<TDto> ReturnNotFound<TDto>(this ControllerBase controller, NotFound _)
            => controller.NotFound();

        public static IActionResult ReturnNotFound(this ControllerBase controller, NotFound _)
            => controller.NotFound();

        public static ActionResult<TDto> ReturnOk<TDto>(this ControllerBase controller, TDto dto)
            => controller.Ok(dto);

        public static IActionResult ReturnOk(this ControllerBase controller, Success _)
            => controller.Ok();
    }

    public class ErrorCodesResult
    {
        public ErrorCodesResult(ErrorCodes codes)
        {
            ErrorCodes = codes;
        }

        public ErrorCodes ErrorCodes { get; }
    }
}
