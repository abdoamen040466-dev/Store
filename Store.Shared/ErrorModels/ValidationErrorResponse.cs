using Microsoft.AspNetCore.Http;

namespace Store.Shared.ErrorModels;

public class ValidationErrorResponse
{
    public int StatusCode { get; set; } = StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; set; } = "Validation Error !!";

    public IEnumerable<ValidationError> Errors { get; set; }
}

public class ValidationError
{
    public string Field { get; set; }
    public IEnumerable<string> Error { get; set; }
}