
namespace SkyVault.Payloads.RequestPayloads;

    public sealed record GetPassportRequest(
        string? SystemUserId,
        string? CustomerProfileId
        );

