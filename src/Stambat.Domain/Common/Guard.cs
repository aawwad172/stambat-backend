using System.Text.RegularExpressions;

namespace Stambat.Domain.Common;

public static class Guard
{
    public static void AgainstNullOrEmpty(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{name} cannot be null or empty.", name);
        }
    }

    public static void AgainstInvalidFormat(string value, string regexPattern, string name)
    {
        if (!Regex.IsMatch(value, regexPattern))
        {
            throw new ArgumentException($"{name} has an invalid format.", name);
        }
    }

    public static void AgainstLength(string value, int min, int max, string name)
    {
        if (value.Length < min || value.Length > max)
        {
            throw new ArgumentException($"{name} must be between {min} and {max} characters.", name);
        }
    }

    public static void AgainstNegative(int value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{name} cannot be negative.", name);
        }
    }

    public static void AgainstNegative(decimal value, string name)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{name} cannot be negative.", name);
        }
    }

    public static void AgainstNegativeOrZero(int value, string name)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{name} must be greater than zero.", name);
        }
    }

    public static void AgainstNegativeOrZero(decimal value, string name)
    {
        if (value <= 0)
        {
            throw new ArgumentException($"{name} must be greater than zero.", name);
        }
    }

    public static void AgainstDefault<T>(T value, string name) where T : struct
    {
        if (value.Equals(default(T)))
        {
            throw new ArgumentException($"{name} cannot be the default value.", name);
        }
    }

    public static void AgainstNull<T>(T? value, string name) where T : class
    {
        if (value is null)
        {
            throw new ArgumentNullException(name, $"{name} cannot be null.");
        }
    }

    public static void AgainstNonUtc(DateTime value, string name)
    {
        if (value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{name} must be in UTC kind.", name);
        }
    }

    public static void AgainstPast(DateTime value, string name)
    {
        if (value <= DateTime.UtcNow)
        {
            throw new ArgumentException($"{name} must be in the future.", name);
        }
    }
}
