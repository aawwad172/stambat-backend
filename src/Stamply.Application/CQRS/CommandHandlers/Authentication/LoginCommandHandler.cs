using Microsoft.Extensions.Logging;

using Stamply.Application.CQRS.Commands.Authentication;
using Stamply.Domain.Common;
using Stamply.Domain.Entities.Identity;
using Stamply.Domain.Entities.Identity.Authentication;
using Stamply.Domain.Exceptions;
using Stamply.Domain.Interfaces.Application.Services;
using Stamply.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stamply.Application.CQRS.CommandHandlers.Authentication;

public class LoginCommandHandler(
    ICurrentUserService currentUserService,
    ILogger<LoginCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    ISecurityService securityService,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtService jwtService) : BaseHandler<LoginCommand, LoginCommandResult>(currentUserService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISecurityService _securityService = securityService;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IJwtService _jwtService = jwtService;

    public override async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            User? user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user is null)
                throw new UnauthenticatedException("Invalid email or password.");

            if (user.IsActive is false)
                throw new NotActiveUserException($"User {user.Id} is not active");

            if (user.IsDeleted is true)
                throw new DeletedUserException($"User {user.Id} is deleted");

            if (user.Credentials is null || !_securityService.VerifySecret(
                        secret: request.Password,
                        secretHash: user.Credentials.PasswordHash
                    )
                )
                throw new UnauthenticatedException("Invalid email or password");

            // All tokens for this session belong to one family ID
            Guid tokenFamilyId = Id.New();

            string accessToken = await _jwtService.GenerateAccessTokenAsync(user);

            RefreshToken refreshToken = _jwtService.CreateRefreshTokenEntity(user, tokenFamilyId);

            await _refreshTokenRepository.AddAsync(refreshToken);

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return new LoginCommandResult(AccessToken: accessToken, RefreshToken: refreshToken.PlaintextToken);
        }
        catch (UnauthenticatedException)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login.");
            await _unitOfWork.RollbackAsync(cancellationToken);
            // Preserve generic message but use the domain exception type used elsewhere.
            throw new UnauthenticatedException("Invalid email or password.");
        }
    }
}
