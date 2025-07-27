namespace NecoTemplate.Domain.Abstractions;

public enum ErrorType
{
    NullValue = 0,
    Failure = 1,
    Validation = 2,
    NotFound = 3,
    Conflict = 4,
    UnAuthorized=5
}
