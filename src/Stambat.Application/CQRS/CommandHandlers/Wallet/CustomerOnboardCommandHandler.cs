using MediatR;

using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Wallet;
using Stambat.Domain.Entities;
using Stambat.Domain.Entities.Identity;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.CommandHandlers.Wallet;

public class CustomerOnboardCommandHandler(
    ILogger<CustomerOnboardCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<CardTemplate> cardTemplateRepository,
    IRepository<WalletPass> walletPassRepository,
    IUserRepository userRepository,
    ITenantRepository tenantRepository,
    IWalletQrTokenService walletQrTokenService,
    IWalletPassProviderFactory walletPassProviderFactory,
    ISecurityService securityService)
    : IRequestHandler<CustomerOnboardCommand, CustomerOnboardCommandResult>
{
    private readonly ILogger<CustomerOnboardCommandHandler> _logger = logger;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<CardTemplate> _cardTemplateRepository = cardTemplateRepository;
    private readonly IRepository<WalletPass> _walletPassRepository = walletPassRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITenantRepository _tenantRepository = tenantRepository;
    private readonly IWalletQrTokenService _walletQrTokenService = walletQrTokenService;
    private readonly IWalletPassProviderFactory _walletPassProviderFactory = walletPassProviderFactory;
    private readonly ISecurityService _securityService = securityService;

    public async Task<CustomerOnboardCommandResult> Handle(
        CustomerOnboardCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            // 1. Load and validate the card template
            CardTemplate cardTemplate = await _cardTemplateRepository.FirstOrDefaultAsync(
                ct => ct.Id == request.CardTemplateId && ct.IsActive)
                ?? throw new NotFoundException($"Card template: {request.CardTemplateId} was not found or is inactive.");

            // 2. Load tenant with profile
            Tenant tenant = await _tenantRepository.GetByIdAsync(cardTemplate.TenantId, new QueryOptions<Tenant>
            {
                Includes = [t => t.TenantProfile!]
            })
                ?? throw new NotFoundException($"Tenant: {cardTemplate.TenantId} was not found.");

            // 3. Find or create customer user
            User? existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            User user;

            if (existingUser is not null)
            {
                user = existingUser;

                // Check if they already have an active or completed pass for this template
                WalletPass? existingPass = await _walletPassRepository.FirstOrDefaultAsync(
                    wp => wp.UserId == user.Id
                        && wp.CardTemplateId == request.CardTemplateId
                        && (wp.Status == WalletPassStatus.Active || wp.Status == WalletPassStatus.Completed));

                if (existingPass is not null)
                    throw new ConflictException("You already have an active loyalty card for this program.");
            }
            else
            {
                // Create new customer user
                string securityStamp = _securityService.GenerateSecureToken();
                user = User.Create(
                    fullName: FullName.Create(request.FirstName, request.LastName),
                    username: request.Email,
                    email: Email.Create(request.Email),
                    securityStamp: securityStamp,
                    isVerified: true); // Customers are auto-verified when onboarding

                await _userRepository.AddAsync(user);
            }

            // 4. Create wallet pass
            WalletPass walletPass = WalletPass.Create(user.Id, cardTemplate.Id, request.WalletProvider);

            // 5. Generate encrypted QR token
            string qrToken = _walletQrTokenService.GenerateQrToken(walletPass.Id, cardTemplate.TenantId);
            walletPass.SetQrToken(qrToken);

            // 6. Create pass via wallet provider
            IWalletPassProvider provider = _walletPassProviderFactory.GetProvider(request.WalletProvider);
            WalletPassResult walletResult = await provider.CreatePassAsync(new WalletPassRequest(
                WalletPassId: walletPass.Id,
                ClassId: cardTemplate.WalletClassId
                    ?? throw new InvalidOperationException("Card template does not have a wallet class configured."),
                TenantId: tenant.Id,
                TenantName: tenant.BusinessName,
                CardTitle: cardTemplate.Title,
                CardDescription: cardTemplate.Description,
                StampsRequired: cardTemplate.StampsRequired,
                CurrentStamps: 0,
                RewardDescription: cardTemplate.RewardDescription,
                QrCodeContent: qrToken,
                LogoUrl: cardTemplate.LogoUrlOverride ?? tenant.TenantProfile?.LogoUrl,
                PrimaryColor: cardTemplate.PrimaryColorOverride ?? tenant.TenantProfile?.PrimaryColor,
                SecondaryColor: cardTemplate.SecondaryColorOverride ?? tenant.TenantProfile?.SecondaryColor,
                TermsAndConditions: cardTemplate.TermsAndConditions), cancellationToken);

            // 7. Set wallet IDs on the pass
            walletPass.SetWalletIds(walletResult.ApplePassSerialNumber, walletResult.GooglePayId);

            await _walletPassRepository.AddAsync(walletPass);

            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new CustomerOnboardCommandResult(
                WalletPassId: walletPass.Id,
                GoogleSaveUrl: walletResult.GoogleSaveUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during customer onboarding: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
