namespace SkyVault.Payloads.RequestPayloads;
public sealed record SearchProfileRequest(
    string? SysUserId,
    string? SearchQuery
    );

