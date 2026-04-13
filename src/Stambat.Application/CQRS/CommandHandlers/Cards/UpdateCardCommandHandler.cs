using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.CommandHandlers.Cards;

public class UpdateCardCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<UpdateCardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository,
    IRepository<WalletPass> walletPassRepository,
    ITenantRepository tenantRepository,
    IWalletPassProviderFactory walletPassProviderFactory)
    : BaseHandler<UpdateCardCommand, UpdateCardCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;
    private readonly IRepository<WalletPass> _walletPassRepository = walletPassRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IWalletPassProviderFactory _walletPassProviderFactory = walletPassProviderFactory;

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

            // Prevent changing RedemptionType if active/completed passes exist
            if (request.RedemptionType != cardTemplate.RedemptionType)
            {
                WalletPass? activePass = await _walletPassRepository.FirstOrDefaultAsync(
                    wp => wp.CardTemplateId == cardTemplate.Id
                        && (wp.Status == WalletPassStatus.Active || wp.Status == WalletPassStatus.Completed));

                if (activePass is not null)
                    throw new BusinessRuleException("Cannot change redemption type while active or completed passes exist.");
            }

            cardTemplate.Update(
                title: request.Title,
                description: request.Description,
                requiredBalance: request.RequiredBalance,
                rewardDescription: request.RewardDescription,
                cardType: request.CardType,
                expiryDurationInDays: request.ExpiryDurationInDays,
                redemptionType: request.RedemptionType,
                pointsPerCurrencyUnit: request.PointsPerCurrencyUnit,
                primaryColorOverride: request.PrimaryColorOverride,
                secondaryColorOverride: request.SecondaryColorOverride,
                logoUrlOverride: request.LogoUrlOverride,
                emptyStampUrl: request.EmptyStampUrl,
                earnedStampUrl: request.EarnedStampUrl,
                termsAndConditions: request.TermsAndConditions,
                isActive: request.IsActive);

            // Update branding in Google Wallet class if it exists
            if (cardTemplate.WalletClassId is not null)
            {
                Tenant tenant = await _tenantRepository.GetByIdAsync(tenantId, new QueryOptions<Tenant>
                {
                    Includes = [t => t.TenantProfile!]
                })
                    ?? throw new NotFoundException($"Tenant: {tenantId} was not found");

                IWalletPassProvider provider = _walletPassProviderFactory.GetProvider(WalletProviderType.Google);
                await provider.UpdateClassAsync(new WalletClassRequest(
                    CardTemplateId: cardTemplate.Id,
                    TenantId: tenantId,
                    TenantName: tenant.BusinessName,
                    CardTitle: cardTemplate.Title,
                    CardDescription: cardTemplate.Description,
                    RequiredBalance: cardTemplate.RequiredBalance,
                    RedemptionType: cardTemplate.RedemptionType,
                    RewardDescription: cardTemplate.RewardDescription,
                    LogoUrl: cardTemplate.LogoUrlOverride ?? tenant.TenantProfile?.LogoUrl,
                    PrimaryColor: cardTemplate.PrimaryColorOverride ?? tenant.TenantProfile?.PrimaryColor,
                    SecondaryColor: cardTemplate.SecondaryColorOverride ?? tenant.TenantProfile?.SecondaryColor),
                    cardTemplate.WalletClassId, cancellationToken);

                // TODO: Update Apple Wallet pass type here when Apple Wallet integration is implemented
            }

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
