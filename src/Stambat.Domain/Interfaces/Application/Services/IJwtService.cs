using System.Security.Claims;

using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;

namespace Stambat.Domain.Interfaces.Application.Services;

public interface IJwtService
{
    Task<string> GenerateAccessTokenAsync(User user);
    Task<string> GenerateAccessTokenForTenantAsync(User user, Guid tenantId);
    RefreshToken CreateRefreshTokenEntity(
        User user,
        Guid tokenFamilyId,
        Guid? tenantId = null);
    Task<ClaimsPrincipal> ValidateToken(string token);
}
