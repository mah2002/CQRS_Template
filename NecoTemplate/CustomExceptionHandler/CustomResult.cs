using Microsoft.AspNetCore.Mvc;
using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.API.CustomExceptionHandler;

public static class CustomResult
{
    public static ActionResult ToActionResult(ErrorResult error)
    {
        if (error == null)
        {
            return new BadRequestObjectResult("ارور ناشناخته ای رخ داده است.");
        }

        return error.Type switch
        {
            ErrorType.NullValue => new BadRequestObjectResult(error.Message),
            ErrorType.NotFound => new NotFoundObjectResult(error.Message),
            ErrorType.Validation => new BadRequestObjectResult(error.Message),
            ErrorType.Conflict => new ConflictObjectResult(error.Message),
            ErrorType.UnAuthorized => new UnauthorizedObjectResult(error.Message),
            _ => new ObjectResult(error.Message) { StatusCode = 500 }
        };
    }
}
