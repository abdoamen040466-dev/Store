namespace Store.Services.Abstractions.Common;

public class Error
{
    public string Code { get; }

    public string Description { get; }

    public ErrorType Type { get; }

    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }

    // static factory methods for creating errors

    public static Error Failure(string code = "General.Failure",
        string description = "A failure has occured.")
        => new(code, description, ErrorType.Failure);

    public static Error Validation(string code = "General.Validation",
        string description = "A Validation error has occured.")
        => new(code, description, ErrorType.Validation);

    public static Error NotFound(string code = "General.NotFound",
        string description = "A 'Not Found' error has occured.")
        => new(code, description, ErrorType.NotFound);

    public static Error Conflict(string code = "General.Conflict",
        string description = "A conflict error has occured.")
        => new(code, description, ErrorType.Conflict);

    public static Error Unauthorized(string code = "General.Unauthorized",
        string description = "The request is not authorized.")
        => new(code, description, ErrorType.Unauthorized);

}


//Error error = Error.Failure();