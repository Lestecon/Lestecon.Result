namespace Lestecon.Result;

public class Error
{
    public Error(Exception exception, string? message = null)
    {
        ArgumentNullException.ThrowIfNull(exception);

        Exception = exception;
        Message = message ?? exception.Message;
    }

    public Error(string message)
    {
        Message = message;
    }

    public Exception? Exception { get; set; }
    public string Message { get; init; }

    public static implicit operator Error(Exception exception) => new(exception);
    public static implicit operator Error(string message) => new(message);
}
