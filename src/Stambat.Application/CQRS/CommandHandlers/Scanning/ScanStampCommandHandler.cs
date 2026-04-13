using Microsoft.Extensions.Logging;

using Stambat.Application.CQRS.Commands.Scanning;
using Stambat.Domain.Entities;
using Stambat.Domain.Enums;
using Stambat.Domain.Exceptions;
using Stambat.Domain.Interfaces.Application.Services;
using Stambat.Domain.Interfaces.Infrastructure.IClients;
using Stambat.Domain.Interfaces.Infrastructure.IRepositories;
namespace Stambat.Application.CQRS.CommandHandlers.Scanning;

public class ScanStampCommandHandler(
    ICurrentUserService currentUserService,
    ITenantProviderService tenantProviderService,
    ILogger<ScanStampCommandHandler> logger,
    IUnitOfWork unitOfWork,
    IRepository<WalletPass> walletPassRepository,
    IWalletQrTokenService walletQrTokenService,
    IWalletPassProviderFactory walletPassProviderFactory)
    : BaseHandler<ScanStampCommand, ScanStampCommandResult>(currentUserService, tenantProviderService, logger, unitOfWork)
{
    private readonly IRepository<WalletPass> _walletPassRepository = walletPassRepository;
    private readonly IWalletQrTokenService _walletQrTokenService = walletQrTokenService;
    private readonly IWalletPassProviderFactory _walletPassProviderFactory = walletPassProviderFactory;

    public override async Task<ScanStampCommandResult> Handle(
        ScanStampCommand request,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            Guid tenantId = _currentTenant.TenantId
                ?? throw new InvalidOperationException("TenantId should be provided via JWT claims.");

            // 1. Decrypt QR token
            var (walletPassId, tokenTenantId) = _walletQrTokenService.DecodeQrToken(request.QrToken);

            // 2. Verify tenant match (prevent cross-tenant scanning)
            if (tokenTenantId != tenantId)
                throw new UnauthorizedException("This loyalty card does not belong to your business.");

            // 3. Load wallet pass with card template
            WalletPass walletPass = await _walletPassRepository.GetByIdAsync(walletPassId, new QueryOptions<WalletPass>
            {
                Includes = [wp => wp.CardTemplate]
            })
                ?? throw new NotFoundException($"Wallet pass: {walletPassId} was not found.");

            // 4. Check and enforce expiry (for Expirable cards)
            if (walletPass.TryEnforceExpiry())
            {
                IWalletPassProvider expiryProvider = _walletPassProviderFactory.GetProvider(walletPass.ProviderType);
                await expiryProvider.UpdatePassAsync(walletPass.BuildUpdateRequest(), cancellationToken);

                _walletPassRepository.Update(walletPass);
                await _unitOfWork.SaveAsync(cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
                throw new BusinessRuleException("This loyalty card has expired and can no longer receive progress.");
            }

            // 5. Validate can add progress
            if (!walletPass.CanAddProgress())
                throw new BusinessRuleException($"Cannot add progress to this card. Current status: {walletPass.Status}");

            // 6. Add progress (domain entity handles conversion and validation)
            walletPass.AddProgress(request.AmountToAdd);

            // 7. Create transaction
            StampTransaction transaction = StampTransaction.Create(
                walletPassId: walletPass.Id,
                merchantId: _currentUser.UserId,
                amountAdded: request.AmountToAdd,
                type: StampTransactionType.Stamp,
                note: request.Note);

            walletPass.Transactions.Add(transaction);

            // 9. Update wallet pass via provider
            IWalletPassProvider provider = _walletPassProviderFactory.GetProvider(walletPass.ProviderType);
            await provider.UpdatePassAsync(walletPass.BuildUpdateRequest(), cancellationToken);

            _walletPassRepository.Update(walletPass);
            await _unitOfWork.SaveAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ScanStampCommandResult(
                WalletPassId: walletPass.Id,
                CurrentBalance: walletPass.CurrentBalance,
                RequiredBalance: walletPass.RequiredBalance,
                RedemptionType: walletPass.RedemptionType,
                IsCompleted: walletPass.Status == WalletPassStatus.Completed,
                CardTitle: walletPass.CardTemplate.Title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing wallet pass: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

}
