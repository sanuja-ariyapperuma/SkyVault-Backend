namespace SkyVault.Payloads.CommonPayloads;

public sealed record Visa(
    string? Id,
    string? VisaNumber,
    string? CountryId,
    string? IssuedPlace,
    string? IssuedDate,
    string? ExpireDate,
    string? AssignedToPrimaryPassport,
    string? PassportNumber,
    string? CountryName
    );

