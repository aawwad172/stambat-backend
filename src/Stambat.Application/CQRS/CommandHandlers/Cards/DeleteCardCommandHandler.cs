using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Cards;

public class DeleteCardCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<DeleteCardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository)
    : BaseHandler<DeleteCardCommand, DeleteCardCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;

    public override async Task<DeleteCardCommandResult> Handle(
        DeleteCardCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            Guid tenantId = _currentTenant.TenantId
                ?? throw new ArgumentException("TenantId should be provided via JWT claims.");

            CardTemplate cardTemplate = await _cardTemplateRepository.FirstOrDefaultAsync(
                ct => ct.Id == request.CardTemplateId && ct.TenantId == tenantId)
                ?? throw new NotFoundException($"Card template: {request.CardTemplateId} was not found");

            cardTemplate.SoftDelete();
            _cardTemplateRepository.Update(cardTemplate);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new DeleteCardCommandResult("Card template deleted successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting card template: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
