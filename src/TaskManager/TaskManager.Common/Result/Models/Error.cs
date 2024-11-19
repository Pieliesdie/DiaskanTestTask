namespace TaskManager.Common.Result.Models;

public class Error
{
    private Error(
        string message,
        ErrorType errorType
    )
    {
        Message = message;
        ErrorType = errorType;
    }

    public ErrorType ErrorType { get; }
    public string Message { get; }
    
    public static Error Failure(string message) => new(message, ErrorType.Failure);
    public static Error NotFound(string message) => new(message, ErrorType.NotFound);
    public static Error Validation(string message) => new(message, ErrorType.Validation);
    public static Error Conflict(string message) => new(message, ErrorType.Conflict);
}