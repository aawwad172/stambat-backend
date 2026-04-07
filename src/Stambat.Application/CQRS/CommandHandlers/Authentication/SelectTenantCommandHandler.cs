using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Authentication;

public class SelectTenantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<SelectTenantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IJwtService jwtService,
    IAuthenticationRepository authenticationRepository)
    : BaseHandler<SelectTenantCommand, SelectTenantCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

    public override async Task<SelectTenantCommandResult> Handle(SelectTenantCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            // 1. Find user by looking up the identity token directly in the DB
            User? user = await _userRepository.GetUserByIdentityTokenAsync(request.IdentityToken);

            if (user is null)
                throw new UnauthenticatedException("Invalid or expired identity token.");

            // 2. Find the matching token entity and validate it
            UserToken? identityToken = user.UserTokens
                .FirstOrDefault(ut =>
                    ut.Type == UserTokenType.TenantSelection &&
                    ut.Token == request.IdentityToken &&
                    !ut.IsUsed &&
                    ut.ExpiryDate > DateTime.UtcNow);

            if (identityToken is null)
                throw new UnauthenticatedException("Invalid or expired identity token.");

            // 3. Mark token as used (one-time use)
            identityToken.MarkAsUsed();

            // 4. Verify user has access to the requested tenant
            List<TenantInfo> userTenants = await _authenticationRepository.GetUserTenantsAsync(user.Id);
            TenantInfo? targetTenant = userTenants.FirstOrDefault(t => t.TenantId == request.TenantId);

            if (targetTenant is null)
                throw new UnauthorizedException("You do not have access to the specified tenant.");

            // 5. Generate tenant-scoped tokens
            Guid tokenFamilyId = IdGenerator.New();
            string accessToken = await _jwtService.GenerateAccessTokenForTenantAsync(user, request.TenantId);
            RefreshToken refreshToken = _jwtService.CreateRefreshTokenEntity(user, tokenFamilyId, request.TenantId);

            user.AddRefreshToken(refreshToken);
            _userRepository.Update(user);

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return new SelectTenantCommandResult(
                AccessToken: accessToken,
                RefreshToken: refreshToken.PlaintextToken,
                TenantId: targetTenant.TenantId,
                TenantName: targetTenant.BusinessName);
        }
        catch (UnauthenticatedException)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        catch (UnauthorizedException)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during tenant selection.");
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
