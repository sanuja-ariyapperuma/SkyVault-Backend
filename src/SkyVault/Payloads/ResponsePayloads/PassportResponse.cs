namespace SkyVault.Payloads.ResponsePayloads;

public sealed record PassportResponse(
    string? Id,
    string? LastName,
    string? OtherNames,
    string? PassportNumber,
    string? GenderId,
    string? DateOfBirth,
    string? PlaceOfBirth,
    string? PassportExpiryDate,
    string? NationalityId,
    string? CountryId,
    string? IsPrimary,
    string? SalutationId
    );
