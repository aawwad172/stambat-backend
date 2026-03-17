namespace Stambat.Domain.Exceptions;

public class InvitationStillActiveException : Exception
{
    public InvitationStillActiveException(string? message) : base(message)
    {
    }

    public InvitationStillActiveException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
