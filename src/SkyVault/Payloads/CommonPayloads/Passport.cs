namespace SkyVault.Payloads.CommonPayloads;

public sealed record Passport(
    string? Id,
    string? LastName,
    string? OtherNames,
    string? PassportNumber,
    string? Gender,
    string? DateOfBirth,
    string? PlaceOfBirth,
    string? ExpiryDate,
    string? NationalityId,
    string? CountryId,
    string? IsPrimary,
    Visa[]? Visa
    );
