using Stambat.Domain.Common;

namespace Stambat.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; private init; }
    public string? MiddleName { get; private init; }
    public string LastName { get; private init; }

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
            firstName.Trim(),
            middleName?.Trim(),
            lastName.Trim());
    }

    public override string ToString() => string.IsNullOrWhiteSpace(MiddleName)
        ? $"{FirstName} {LastName}"
        : $"{FirstName} {MiddleName} {LastName}";

    public string Formatted => string.Join(" ", new[] { FirstName, MiddleName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));
}
