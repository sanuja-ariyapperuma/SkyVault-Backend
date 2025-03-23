
namespace SkyVault.Payloads.RequestPayloads;

public sealed record GetProfileRequest(
    string? id,
    string? sysUserId
    );

