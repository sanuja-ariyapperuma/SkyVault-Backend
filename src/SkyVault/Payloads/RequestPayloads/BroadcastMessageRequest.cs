namespace SkyVault.Payloads.RequestPayloads
{
    public record BroadcastMessageRequest(
        string Subject,
        string Content,
        string BroadcastType,
        string? FileName,
        bool IsEmergency = false
    );
}
