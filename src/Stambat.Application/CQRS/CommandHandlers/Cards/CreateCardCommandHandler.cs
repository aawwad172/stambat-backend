using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Cards;
using Stambat.Application.Utilities;
using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.CommandHandlers.Cards;

public class CreateCardCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<CreateCardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository,
    ITenantRepository tenantRepository,
    IWalletPassProviderFactory walletPassProviderFactory,
    IQrCodeService qrCodeService,
    IConfiguration configuration)
    : BaseHandler<CreateCardCommand, CreateCardCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IWalletPassProviderFactory _walletPassProviderFactory = walletPassProviderFactory;
    private readonly IQrCodeService _qrCodeService = qrCodeService;
    private readonly string _baseUrl = configuration.GetRequiredSetting("Stambat:BaseUrl");

    public override async Task<CreateCardCommandResult> Handle(
        CreateCardCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            Guid tenantId = _currentTenant.TenantId
                ?? throw new ArgumentException("TenantId should be provided via JWT claims.");

            Tenant tenant = await _tenantRepository.GetByIdAsync(tenantId, new QueryOptions<Tenant>
            {
                Includes = [t => t.TenantProfile!]
            })
                ?? throw new NotFoundException($"Tenant: {tenantId} was not found");

            CardTemplate? existing = await _cardTemplateRepository.FirstOrDefaultAsync(
                ct => ct.TenantId == tenantId && ct.Title == request.Title);

            if (existing is not null)
                throw new ConflictException($"A card template with the title '{request.Title}' already exists for this tenant.");

            CardTemplate cardTemplate = CardTemplate.Create(
                tenantId: tenantId,
                title: request.Title,
                description: request.Description,
                stampsRequired: request.StampsRequired,
                rewardDescription: request.RewardDescription,
                primaryColorOverride: request.PrimaryColorOverride,
                secondaryColorOverride: request.SecondaryColorOverride,
                logoUrlOverride: request.LogoUrlOverride,
                emptyStampUrl: request.EmptyStampUrl,
                earnedStampUrl: request.EarnedStampUrl,
                termsAndConditions: request.TermsAndConditions);

            string slug = tenant.TenantProfile?.Slug ?? tenantId.ToString();
            string joinUrl = $"{_baseUrl}/join/{slug}/{cardTemplate.Id}";
            string joinQrCodeBase64 = _qrCodeService.GenerateQrCodeBase64(joinUrl);
            cardTemplate.SetJoinInfo(joinUrl, joinQrCodeBase64);

            // Create loyalty class in wallet provider (Google Wallet)
            IWalletPassProvider provider = _walletPassProviderFactory.GetProvider(WalletProviderType.Google);
            WalletClassResult classResult = await provider.CreateClassAsync(new WalletClassRequest(
                CardTemplateId: cardTemplate.Id,
                TenantId: tenantId,
                TenantName: tenant.BusinessName,
                CardTitle: cardTemplate.Title,
                CardDescription: cardTemplate.Description,
                StampsRequired: cardTemplate.StampsRequired,
                RewardDescription: cardTemplate.RewardDescription,
                LogoUrl: cardTemplate.LogoUrlOverride ?? tenant.TenantProfile?.LogoUrl,
                PrimaryColor: cardTemplate.PrimaryColorOverride ?? tenant.TenantProfile?.PrimaryColor,
                SecondaryColor: cardTemplate.SecondaryColorOverride ?? tenant.TenantProfile?.SecondaryColor), cancellationToken);

            cardTemplate.SetWalletClassId(classResult.ClassId);

            // TODO: Create Apple Wallet pass type here when Apple Wallet integration is implemented

            await _cardTemplateRepository.AddAsync(cardTemplate);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new CreateCardCommandResult(cardTemplate.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating card template: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
