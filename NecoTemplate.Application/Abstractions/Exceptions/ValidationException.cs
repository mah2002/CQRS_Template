using NecoTemplate.Domain.Abstractions;

namespace NecoTemplate.Application.Abstractions.Exceptions;

public sealed class ValidationException:Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }

}
