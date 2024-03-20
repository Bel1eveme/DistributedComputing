using Forum.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ValidationException = FluentValidation.ValidationException;

namespace Forum.Api.Controllers;

public class ExceptionController : ControllerBase
{
    [Route("/error")]
    [NonAction]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException)
        {
            return ValidationProblem(title: exception.Message, statusCode: 403);
        }

        return Problem();
    }
}