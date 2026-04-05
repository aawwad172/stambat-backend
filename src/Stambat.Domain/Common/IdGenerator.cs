namespace Stambat.Domain.Common;

public static class IdGenerator
{
    public static Guid New() => Guid.CreateVersion7();
}
