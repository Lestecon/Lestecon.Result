using System.Diagnostics.CodeAnalysis;

namespace Lestecon.Result;
public class Result
{
    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public Result(Error error)
        : this(isSuccess: false)
    {
        Error = error;
    }

    public virtual bool IsSuccess { get; init; }
    public Error? Error { get; init; }

    public static Result Fail() => new(isSuccess: false);
    public static Result Fail(Exception exception, string? message = null) => new(new Error(exception, message));
    public static Result Fail(string message) => new(new Error(message));
    public static Result Success() => new(isSuccess: true);
    public static Result<T> Success<T>(T value) => new(value);

    public static implicit operator Result(Error error) => new(error);
    public static implicit operator Result(Exception exception) => new(exception);
    public static implicit operator Result(string message) => new(message);
}

public class Result<T> : Result
{
    public Result(T value)
        : base(isSuccess: true)
    {
        Value = value;
    }

    public Result(bool isSuccess)
        : base(isSuccess)
    {
    }

    public Result(Error error)
        : base(error)
    {
        Error = error;
    }

    public T? Value { get; init; }

    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess { get; init; }

    public bool TryGetValue([NotNullWhen(true)] out T? value, out Error? error)
    {
        error = Error;
        return TryGetValue(out value);
    }

    public bool TryGetValue([NotNullWhen(true)] out T? value)
    {
        value = IsSuccess ? Value : default;
        return IsSuccess;
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(error);
    public static implicit operator Result<T>(Exception exception) => new(exception);
    public static implicit operator Result<T>(string message) => new(message);
}
