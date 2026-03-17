namespace Stambat.Domain.ValueObjects;

public class Email
{
    public required string To { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}
