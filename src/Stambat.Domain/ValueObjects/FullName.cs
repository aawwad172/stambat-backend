namespace Stambat.Domain.ValueObjects;

public class FullName
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
}
