namespace Stambat.Domain.Exceptions;

public class UserNotVerifiedException : Exception
{
    public UserNotVerifiedException(string? message) : base(message)
    {
    }

    public UserNotVerifiedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
