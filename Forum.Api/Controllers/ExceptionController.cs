using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers;

public class ExceptionController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException)
        {
            return ValidationProblem(title: exception.Message, statusCode: 303);
        }

        return Problem();
    }
}