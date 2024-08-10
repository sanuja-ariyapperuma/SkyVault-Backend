namespace SkyVault.Payloads.CommonPayloads
{
    public sealed record ProfilePayload(
    string? Id,
    string? SalutationId,
    PassportModal[]? Passports,
    string[]? FrequentFlyerNumbers,
    string? PreffdComMth,
    string? ParentId,
    string? SystemUserId,
    Visa[]? Visas
        );
}
