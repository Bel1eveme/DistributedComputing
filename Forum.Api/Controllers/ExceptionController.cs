using Forum.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
        
        if (exception is DbUpdateException)
        {
            return ValidationProblem(title: exception.Message, statusCode: 409);
        }
        
        if (exception is PostgresException { SqlState: "23503" })
        {
            return Problem(title: "Foreign key constraint violation", statusCode: 409);
        }
        
        if (exception is NpgsqlException)
        {
            return Problem(title: "Foreign key constraint violation", statusCode: 409);
        }

        return Problem();
    }
}