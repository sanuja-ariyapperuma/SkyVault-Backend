namespace SkyVault.Payloads.RequestPayloads
{
    public class FFNRequest
    {
        public string? CustomerProfileId { get; set; }
        public string? FFN { get; set; } = String.Empty;
    }
}
