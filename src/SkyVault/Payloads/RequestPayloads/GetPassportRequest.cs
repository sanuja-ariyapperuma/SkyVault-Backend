
namespace SkyVault.Payloads.RequestPayloads;

    public sealed record GetPassportRequest(
        string? id, 
        string? sysUserId,
        string? passportNumber
        );

