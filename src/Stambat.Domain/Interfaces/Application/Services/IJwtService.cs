using System.Security.Claims;

using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Domain.Interfaces.Application.Services;

public interface IJwtService
{
    Task<string> GenerateAccessTokenAsync(User user);
    RefreshToken CreateRefreshTokenEntity(
        User user,
        Guid tokenFamilyId);
    Task<ClaimsPrincipal> ValidateToken(string token);
}
