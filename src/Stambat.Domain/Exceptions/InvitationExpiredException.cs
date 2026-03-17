using System;
using System.Runtime.Serialization;

namespace Stambat.Domain.Exceptions;

public class InvitationExpiredException : Exception
{
    public InvitationExpiredException(string? message) : base(message)
    {
    }

    public InvitationExpiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
