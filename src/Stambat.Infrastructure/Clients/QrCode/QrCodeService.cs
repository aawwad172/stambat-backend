using QRCoder;

using Stambat.Domain.Interfaces.Infrastructure.IClients;

namespace Stambat.Infrastructure.Clients.QrCode;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQrCodePng(string content, int pixelSize = 300)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(content);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pixelSize);

        int pixelsPerModule = Math.Max(1, (int)Math.Ceiling(pixelSize / 33d));

        using QRCodeGenerator qrGenerator = new();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
        using PngByteQRCode pngQrCode = new(qrCodeData);
        return pngQrCode.GetGraphic(pixelsPerModule);
    }

    public string GenerateQrCodeBase64(string content, int pixelSize = 300)
    {
        byte[] pngBytes = GenerateQrCodePng(content, pixelSize);
        return Convert.ToBase64String(pngBytes);
    }
}
