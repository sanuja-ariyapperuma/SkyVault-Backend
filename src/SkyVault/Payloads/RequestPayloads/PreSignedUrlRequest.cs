using SkyVault.Payloads.CommonPayloads;

namespace SkyVault.Payloads.RequestPayloads
{
    public record PreSignedUrlRequest(string FileType, string MessageType)
    {
        public FileType FileTypeEnum => Enum.Parse<FileType>(FileType, true);
        public MessageType MessageTypeEnum => Enum.Parse<MessageType>(MessageType, true);
    };
}
