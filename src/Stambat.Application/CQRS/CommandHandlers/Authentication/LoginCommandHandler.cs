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

public class LoginCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<LoginCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    ISecurityService securityService,
    IJwtService jwtService,
    IAuthenticationRepository authenticationRepository) : BaseHandler<LoginCommand, LoginCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISecurityService _securityService = securityService;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

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

            // Check if user is a SuperAdmin (global role, TenantId = NULL)
            bool isSuperAdmin = await _authenticationRepository.IsUserInRoleAsync(user.Id, "SuperAdmin");

            if (isSuperAdmin)
            {
                // SuperAdmin: issue tokens directly, skip tenant selection
                Guid tokenFamilyId = IdGenerator.New();
                string accessToken = await _jwtService.GenerateAccessTokenAsync(user);
                RefreshToken refreshToken = _jwtService.CreateRefreshTokenEntity(user, tokenFamilyId);

                user.AddRefreshToken(refreshToken);
                _userRepository.Update(user);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return new LoginCommandResult(
                    IdentityToken: null,
                    AccessToken: accessToken,
                    RefreshToken: refreshToken.PlaintextToken,
                    IsSuperAdmin: true,
                    Tenants: []);
            }
            else
            {
                // Regular user: generate identity token + return tenant list
                string identityTokenPlaintext = _securityService.GenerateSecureToken();
                DateTime expiry = DateTime.UtcNow.AddMinutes(5);

                user.AddUserToken(UserTokenType.TenantSelection, identityTokenPlaintext, expiry);
                _userRepository.Update(user);

                List<TenantInfo> tenants = await _authenticationRepository.GetUserTenantsAsync(user.Id);

                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();

                return new LoginCommandResult(
                    IdentityToken: identityTokenPlaintext,
                    AccessToken: null,
                    RefreshToken: null,
                    IsSuperAdmin: false,
                    Tenants: tenants);
            }
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
