using Stambat.Domain.Common;

namespace Stambat.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string LastName { get; init; }

#pragma warning disable CS8618
    // Add this for the Serializer and EF Core
    private FullName() { }
#pragma warning restore CS8618 

    private FullName(string firstName, string? middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName, string? middleName = null)
    {
        Guard.AgainstNullOrEmpty(firstName, nameof(firstName));
        Guard.AgainstNullOrEmpty(lastName, nameof(lastName));

        return new FullName(
            firstName.Trim().ToLowerInvariant(),
            middleName?.Trim().ToLowerInvariant(),
            lastName.Trim().ToLowerInvariant());
    }

    public override string ToString() => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";
}
