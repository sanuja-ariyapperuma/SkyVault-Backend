namespace SkyVault.Payloads.RequestPayloads;
public record VisaReqeust(
    string? Id,
    string? CustomerProfileId,
    string? PassportId,
    string? VisaNumber,
    string? IssuedPlace,
    string? IssuedDate,
    string? ExpiryDate,
    string? CountryId,
    string? BirthPlace,
    string? DestinationCountryId
);

