namespace Stambat.Domain.Constants;

/// <summary>
/// Defines custom claim types used in the application's JWT for authorization.
/// </summary>
public static class CustomClaims
{
    // This is the key we use to store all granular permissions in the JWT payload.
    public const string Permission = "permissions";

    // Other custom claims can go here, like user's full name, etc.
    public const string FullName = "full_name";
}
