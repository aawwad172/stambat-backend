using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Tenants;
using Stambat.Domain.Entities;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Tenants;

public class CancelInvitationCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<CancelInvitationCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IInvitationRepository invitationRepository)
    : BaseHandler<CancelInvitationCommand, CancelInvitationCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IInvitationRepository _invitationRepository = invitationRepository;

    public override async Task<CancelInvitationCommandResult> Handle(CancelInvitationCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (_currentTenant.TenantId is null)
                throw new ArgumentException("TenantId should be provided via JWT claims.");

            Guid tenantId = _currentTenant.TenantId.Value;

            Invitation invitation = await _invitationRepository.GetByIdForTenantAsync(request.InvitationId, tenantId)
                ?? throw new NotFoundException($"Invitation {request.InvitationId} was not found in this tenant.");

            if (invitation.IsUsed)
                throw new InvalidOperationException("This invitation has already been used and cannot be cancelled.");

            if (invitation.IsCancelled)
                throw new InvalidOperationException("This invitation has already been cancelled.");

            invitation.Cancel();

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new CancelInvitationCommandResult("Invitation cancelled successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while cancelling invitation: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
