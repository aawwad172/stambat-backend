using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;

namespace Stambat.Application.CQRS.CommandHandlers.Cards;

public class UpdateCardCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<UpdateCardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository)
    : BaseHandler<UpdateCardCommand, UpdateCardCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;

    public override async Task<UpdateCardCommandResult> Handle(
        UpdateCardCommand request,
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

            // Check for duplicate title if title changed
            if (!string.Equals(cardTemplate.Title, request.Title, StringComparison.Ordinal))
            {
                CardTemplate? duplicate = await _cardTemplateRepository.FirstOrDefaultAsync(
                    ct => ct.TenantId == tenantId && ct.Title == request.Title && ct.Id != request.CardTemplateId);

                if (duplicate is not null)
                    throw new ConflictException($"A card template with the title '{request.Title}' already exists for this tenant.");
            }

            cardTemplate.Update(
                title: request.Title,
                description: request.Description,
                stampsRequired: request.StampsRequired,
                rewardDescription: request.RewardDescription,
                primaryColorOverride: request.PrimaryColorOverride,
                secondaryColorOverride: request.SecondaryColorOverride,
                logoUrlOverride: request.LogoUrlOverride,
                emptyStampUrl: request.EmptyStampUrl,
                earnedStampUrl: request.EarnedStampUrl,
                termsAndConditions: request.TermsAndConditions,
                isActive: request.IsActive);

            _cardTemplateRepository.Update(cardTemplate);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new UpdateCardCommandResult("Card template updated successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating card template: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
