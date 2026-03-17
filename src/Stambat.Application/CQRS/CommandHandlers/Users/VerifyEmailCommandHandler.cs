using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Users;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

using UserEntity = Stambat.Domain.Entities.Identity.User;

namespace Stambat.Application.CQRS.CommandHandlers.Users;

public class VerifyEmailCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<VerifyEmailCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserTokenRepository userTokenRepository,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IJwtService jwtService)
    : BaseHandler<VerifyEmailCommand, VerifyEmailCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUserTokenRepository _userTokenRepository = userTokenRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly IJwtService _jwtService = jwtService;

    public override async Task<VerifyEmailCommandResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        UserEntity? user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new NotFoundException($"User with the Id: {request.UserId} was not found");

        UserToken? token = await _userTokenRepository.GetUserTokenByTokenAsync(request.Token);

        if (token is null)
            throw new NotFoundException($"Token {request.Token} is not available");

        if (token.IsUsed || token.ExpiryDate < DateTime.UtcNow || token.Type != UserTokenType.EmailVerification)
            throw new InvalidTokenException("Token is used or expired");

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            user.IsVerified = true;
            token.IsUsed = true;

            _userRepository.Update(user);
            _userTokenRepository.Update(token);


            Guid tokenFamilyId = Id.New();
            string accessToken = await _jwtService.GenerateAccessTokenAsync(user);
            RefreshToken refreshtoken = _jwtService.CreateRefreshTokenEntity(user, tokenFamilyId);

            await _refreshTokenRepository.AddAsync(refreshtoken);


            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new VerifyEmailCommandResult(AccessToken: accessToken, RefreshToken: refreshtoken.PlaintextToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong when verifying email for user: {request.UserId}", ex.Message);
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
