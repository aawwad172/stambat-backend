
using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Authentication;

public class LogoutCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<LogoutCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ISecurityService securityService,
    IUserRepository userRepository)
    : BaseHandler<LogoutCommand, LogoutCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly ISecurityService _securityService = securityService;
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<LogoutCommandResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                throw new UnauthorizedException("Refresh token is required.");

            User? user = await _userRepository.GetByIdWithDetailsAsync(_currentUser.UserId);

            if (user is null)
                throw new NotFoundException("Cannot find user");

            // 1. --- Find and Verify Token ---
            // We need to find the token in the user's collection by hashing the incoming plaintext
            var token = user.RefreshTokens.FirstOrDefault(rt => _securityService.VerifySecret(request.RefreshToken, rt.TokenHash));

            if (token is null || token.IsRevoked || token.IsExpired)
                throw new UnauthorizedException("Session is already invalid or token not found.");


            // 2. --- Revoke and Audit ---
            user.RevokeRefreshToken(token.TokenHash, "Manual Logout");

            // --------------------------------------------------------------------------
            // CRITICAL: Rotate the SecurityStamp to invalidate all active Access Tokens
            // The next time an active Access Token is presented, the SecurityStamp check
            // in the JwtService.ValidateToken method will fail.
            // --------------------------------------------------------------------------
            user.UpdateSecurityStamp();


            // Persist user changes (this handles both security stamp and token revocation)
            _userRepository.Update(user);

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return new LogoutCommandResult("Logout successful");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during logout.");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
