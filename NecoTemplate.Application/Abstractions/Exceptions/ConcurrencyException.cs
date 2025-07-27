namespace NecoTemplate.Application.Abstractions.Exceptions;

public sealed class ConcurrencyException : Exception
{
    public ConcurrencyException(string message, Exception error):base(message,error)
    {

    }
}
