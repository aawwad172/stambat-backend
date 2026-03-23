
using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Authentication;

public class RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<RefreshTokenCommandHandler> logger,
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    IUserRepository userRepository,
    ISecurityService securityService,
    IJwtService jwtService)
    : BaseHandler<RefreshTokenCommand, RefreshTokenCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISecurityService _securityService = securityService;
    private readonly IJwtService _jwtService = jwtService;

    public override async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdWithDetailsAsync(_currentUser.UserId);
        if (user is null)
            throw new NotFoundException("User not found");

        RefreshToken? oldToken = user.RefreshTokens.FirstOrDefault(rt => _securityService.VerifySecret(request.RefreshToken, rt.TokenHash));

        if (oldToken is null || !oldToken.IsActive)
            throw new UnauthenticatedException("Invalid or expired refresh token.");

        if (oldToken.SecurityStampAtIssue != user.SecurityStamp)
            throw new UnauthenticatedException("Session security has been compromised. Please log in again.");

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            //  Generate a new refresh token
            RefreshToken newRefreshToken = _jwtService.CreateRefreshTokenEntity(user, oldToken.TokenFamilyId);

            user.RevokeRefreshToken(oldToken.TokenHash, "Rotated");

            user.AddRefreshToken(
                newRefreshToken.TokenHash,
                newRefreshToken.PlaintextToken,
                newRefreshToken.ExpiresAt,
                newRefreshToken.TokenFamilyId
            );

            _userRepository.Update(user);

            // Generate a new access token with desired expiration
            string newAccessToken = await _jwtService.GenerateAccessTokenAsync(user);

            // Save all changes and commit the transaction
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            // 4. --- Return New Tokens ---
            // newRefreshTokenEntity.PlaintextToken is available because of the [NotMapped] property.
            return new RefreshTokenCommandResult(newAccessToken, newRefreshToken.PlaintextToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("An error occurred during rotating token: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync();
            throw;
        }

    }
}
