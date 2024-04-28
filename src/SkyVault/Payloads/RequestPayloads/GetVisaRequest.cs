namespace SkyVault.Payloads.RequestPayloads;

public sealed record GetVisaRequest(
    string? Id,
    string? systemUserId
);