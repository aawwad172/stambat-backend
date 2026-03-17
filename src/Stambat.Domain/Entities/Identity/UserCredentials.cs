using Stambat.Domain.Interfaces.Domain;

namespace Stambat.Domain.Entities.Identity;

public class UserCredentials : IEntity
{
    public Guid Id { get; init; }
    public required string PasswordHash { get; set; }
    public required Guid UserId { get; set; }
    public virtual User? User { get; set; }
}
