using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Tasks.Endpoints;

/// <summary>
/// Represents a base controller for the Web API.
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        var statusCode = error.ErrorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(
            statusCode: statusCode,
            title: error.ErrorType.ToString(),
            detail: error.Message);
    }
}