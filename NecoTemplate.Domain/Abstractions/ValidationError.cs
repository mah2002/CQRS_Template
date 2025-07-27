namespace NecoTemplate.Domain.Abstractions;

public sealed record ValidationErrors : ErrorResult
{
    public ValidationErrors(IEnumerable<ValidationError> errors)
        : base(
            "General.Validation",
            "One or more validation errors occurred",
            ErrorType.Validation)
    {
        Errors = errors;
    }
    public IEnumerable<ValidationError> Errors { get; } 

}

public sealed record ValidationError(string PropertyName, string ErrorMessage);