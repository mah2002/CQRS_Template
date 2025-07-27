using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.CodeAnalysis;

namespace NecoTemplate.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess, ErrorResult error)
    {
        //if (isSuccess && error != ErrorResult.NoError ||
        //    !isSuccess && error == ErrorResult.NoError)
        //{
        //    throw new ArgumentException("Invalid error", nameof(error));
        //}

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public ErrorResult Error { get; }

    public static Result Success() => new(true, ErrorResult.NoError);
    public static Result Failure(ErrorResult error) => new(false, error);


    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, ErrorResult.NoError);

    public static Result<TValue> Failure<TValue>(ErrorResult error) =>
        new(default, false, error);
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, ErrorResult error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(ErrorResult.NullValue);

    public static Result<TValue> ValidationFailure(ErrorResult error) =>
        new(default, false, error);
}
