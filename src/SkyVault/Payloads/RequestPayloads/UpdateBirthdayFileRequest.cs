using SkyVault.Payloads.CommonPayloads;

namespace SkyVault.Payloads.RequestPayloads
{
    public record UpdateBirthdayFileRequest(string FileName, MessageType MessageType);
}
