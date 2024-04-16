namespace SkyVault.Payloads.ResponsePayloads;

public record SearchProfileItem(
    int? ProfileId,
    string? LastName,
    string? OtherNames,
    string? PassportNumber,
    string? Salutation);