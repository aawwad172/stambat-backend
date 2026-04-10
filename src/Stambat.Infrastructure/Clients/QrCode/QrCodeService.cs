using QRCoder;

using Stambat.Domain.Interfaces.Infrastructure.IClients;

namespace Stambat.Infrastructure.Clients.QrCode;

public class QrCodeService : IQrCodeService
{
    public byte[] GenerateQrCodePng(string content, int pixelSize = 300)
    {
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
        using PngByteQRCode pngQrCode = new PngByteQRCode(qrCodeData);
        return pngQrCode.GetGraphic(pixelSize / 33); // ~33 modules for QR, pixelsPerModule
    }

    public string GenerateQrCodeBase64(string content, int pixelSize = 300)
    {
        byte[] pngBytes = GenerateQrCodePng(content, pixelSize);
        return Convert.ToBase64String(pngBytes);
    }
}
