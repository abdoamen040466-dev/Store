using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Store.Services.Abstractions.Common;

namespace Store.Presentation.API;

[ApiController]
[Route("api/[controller]")]
public class APIBaseController : ControllerBase
{
    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        return Problem(result.Errors);
    }

    protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
    {
        if (result.IsSuccess)
            return result.Value;

        return Problem(result.Errors);
    }

    protected ActionResult Problem(IReadOnlyList<Error> errors)
    {
        if (errors.Count == 0)
        {
            return Problem(statusCode: 500, title: "Unexpected error occurred");
        }
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return HandleValidationProblem(errors);
        }

        return HandleSingleErrorProblem(errors[0]);
    }


    private ActionResult HandleSingleErrorProblem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return base.Problem(
                statusCode: statusCode,
                title: error.Description,
                type: error.Code
            );
    }
    private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelState);
    }
}
