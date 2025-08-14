using QRCoder;

public class QRCodeService
{
    public string GenerateSvgQRCode(string text)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new SvgQRCode(qrCodeData);
        string svg = qrCode.GetGraphic(5);

        // Convert SVG to data URI
        string svgDataUri = "data:image/svg+xml;utf8," + Uri.EscapeDataString(svg);
        return svgDataUri;
    }
}