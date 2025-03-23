namespace SkyVault.Payloads.ResponsePayloads
{
    public class Message
    {
        public string? Content { get; set; }
        public string? FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
