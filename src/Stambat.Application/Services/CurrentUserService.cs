using Stambat.Domain.Interfaces.Application.Services;

namespace Stambat.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; set; }
}
