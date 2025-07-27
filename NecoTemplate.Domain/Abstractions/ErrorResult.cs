using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NecoTemplate.Domain.Abstractions;

public record ErrorResult
{
    public static readonly ErrorResult NoError = new("Success", "The process has done successfully.");

    public static readonly ErrorResult NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.NullValue);

    public ErrorResult(string code, string description, ErrorType type)
    {
        Code = code;
        Message = description;
        Type = type;
    }

    private ErrorResult(string code, string description)
    {
        Code = code;
        Message = description;
    }

    public string Code { get; }

    public string Message { get; }

    public ErrorType Type { get; }

    public static ErrorResult Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    public static ErrorResult NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);


    public static ErrorResult Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    public static ErrorResult UnAuthorized(string code, string description) =>
        new(code, description, ErrorType.UnAuthorized);
}
