using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Scanning;
using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
using Stambat.Domain.ValueObjects;

namespace Stambat.Application.CQRS.CommandHandlers.Scanning;

public class ScanRedeemCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<ScanRedeemCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<WalletPass> walletPassRepository,
    IWalletQrTokenService walletQrTokenService,
    IWalletPassProviderFactory walletPassProviderFactory)
    : BaseHandler<ScanRedeemCommand, ScanRedeemCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<WalletPass> _walletPassRepository = walletPassRepository;
    private readonly IWalletQrTokenService _walletQrTokenService = walletQrTokenService;
    private readonly IWalletPassProviderFactory _walletPassProviderFactory = walletPassProviderFactory;

    public override async Task<ScanRedeemCommandResult> Handle(
        ScanRedeemCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            Guid tenantId = _currentTenant.TenantId
                ?? throw new InvalidOperationException("TenantId should be provided via JWT claims.");

            // 1. Decrypt QR token
            var (walletPassId, tokenTenantId) = _walletQrTokenService.DecodeQrToken(request.QrToken);

            // 2. Verify tenant match
            if (tokenTenantId != tenantId)
                throw new UnauthorizedException("This loyalty card does not belong to your business.");

            // 3. Load wallet pass with card template
            WalletPass walletPass = await _walletPassRepository.GetByIdAsync(walletPassId, new QueryOptions<WalletPass>
            {
                Includes = [wp => wp.CardTemplate]
            })
                ?? throw new NotFoundException($"Wallet pass: {walletPassId} was not found.");

            // 4. Validate can redeem
            if (!walletPass.CanRedeem())
                throw new BusinessRuleException($"Cannot redeem this card. Current status: {walletPass.Status}");

            // 5. Redeem
            walletPass.Redeem();

            // 6. Create redeem transaction
            StampTransaction transaction = StampTransaction.Create(
                walletPassId: walletPass.Id,
                merchantId: _currentUser.UserId,
                stampsAdded: 0,
                type: StampTransactionType.Redeem,
                note: $"Reward redeemed: {walletPass.CardTemplate.RewardDescription}");

            walletPass.Transactions.Add(transaction);

            // 7. Update wallet pass via provider
            IWalletPassProvider provider = _walletPassProviderFactory.GetProvider(walletPass.ProviderType);

            await provider.UpdatePassAsync(new WalletPassUpdateRequest(
                WalletPassId: walletPass.Id,
                ApplePassSerialNumber: walletPass.ApplePassSerialNumber,
                GooglePayId: walletPass.GooglePayId,
                CurrentStamps: walletPass.CurrentStamps,
                StampsRequired: walletPass.CardTemplate.StampsRequired,
                Status: walletPass.Status,
                QrCodeContent: walletPass.QrTokenPayload,
                LogoUrl: walletPass.CardTemplate.LogoUrlOverride,
                PrimaryColor: walletPass.CardTemplate.PrimaryColorOverride,
                SecondaryColor: walletPass.CardTemplate.SecondaryColorOverride), cancellationToken);

            _walletPassRepository.Update(walletPass);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ScanRedeemCommandResult(
                WalletPassId: walletPass.Id,
                CardTitle: walletPass.CardTemplate.Title,
                RewardDescription: walletPass.CardTemplate.RewardDescription,
                RedeemedAt: walletPass.RedeemedAt!.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while redeeming wallet pass: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

}
