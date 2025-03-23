namespace SkyVault.Payloads.RequestPayloads;
public record PassportRequest(
        string? Id,
        string? CustomerProfileId,
        string? ParentId,
        string? LastName,
        string? OtherNames,
        string? PassportNumber,
        string? Gender,
        string? DateOfBirth,
        string? ExpiryDate,
        string? NationalityId,
        string? CountryId,
        string? IsPrimary,
        string? SalutationId
    );
