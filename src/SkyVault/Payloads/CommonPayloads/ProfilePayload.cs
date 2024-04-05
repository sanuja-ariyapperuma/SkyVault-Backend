namespace SkyVault.Payloads.CommonPayloads
{
    public sealed record ProfilePayload(
    string? Id,
    string? SalutationId,
    Passport[]? Passports,
    string[]? FrequentFlyerNumbers,
    string? PreffdComMth,
    string? ParentId,
    string? SystemUserId
        );
}
