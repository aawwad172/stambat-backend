using Stambat.Domain.Common;

namespace Stambat.Domain.ValueObjects;

public sealed record Email
{
    private const string EmailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public string Value { get; init; }

    private Email(string value)
    {
        Value = value;
    }

#pragma warning disable CS8618
    public Email() { }
#pragma warning restore CS8618

    public static Email Create(string value)
    {
        Guard.AgainstNullOrEmpty(value, nameof(value));

        var processedValue = value.Trim().ToLowerInvariant();

        Guard.AgainstInvalidFormat(processedValue, EmailRegex, nameof(value));

        return new Email(processedValue);
    }

    public static implicit operator string(Email email) => email.Value;
    public static explicit operator Email(string value) => Create(value);

    public override string ToString() => Value;
}
