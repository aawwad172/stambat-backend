using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Authentication;
using Stambat.Domain.Common;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Entities.Identity.Authentication;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Authentication;

public class SwitchTenantCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<SwitchTenantCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IJwtService jwtService,
    IAuthenticationRepository authenticationRepository)
    : BaseHandler<SwitchTenantCommand, SwitchTenantCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IAuthenticationRepository _authenticationRepository = authenticationRepository;

    public override async Task<SwitchTenantCommandResult> Handle(SwitchTenantCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            // 1. Get the currently authenticated user
            User? user = await _userRepository.GetByIdWithDetailsAsync(_currentUser.UserId);
            if (user is null)
                throw new NotFoundException("User not found.");

            // 2. Verify user has access to the requested tenant
            List<TenantInfo> userTenants = await _authenticationRepository.GetUserTenantsAsync(user.Id);
            TenantInfo? targetTenant = userTenants.FirstOrDefault(t => t.TenantId == request.TenantId);

            if (targetTenant is null)
                throw new UnauthorizedException("You do not have access to the specified tenant.");

            // 3. Generate new tenant-scoped tokens
            // NOTE: We do NOT revoke the old tenant's refresh token (keep old sessions alive)
            Guid tokenFamilyId = IdGenerator.New();
            string accessToken = await _jwtService.GenerateAccessTokenForTenantAsync(user, request.TenantId);
            RefreshToken refreshToken = _jwtService.CreateRefreshTokenEntity(user, tokenFamilyId, request.TenantId);

            user.AddRefreshToken(refreshToken);
            _userRepository.Update(user);

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return new SwitchTenantCommandResult(
                AccessToken: accessToken,
                RefreshToken: refreshToken.PlaintextToken,
                TenantId: targetTenant.TenantId,
                TenantName: targetTenant.BusinessName);
        }
        catch (Exception ex) when (ex is not NotFoundException and not UnauthorizedException)
        {
            _logger.LogError(ex, "An error occurred during tenant switching.");
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
