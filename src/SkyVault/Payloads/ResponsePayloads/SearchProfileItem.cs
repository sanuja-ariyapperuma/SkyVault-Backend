namespace SkyVault.Payloads.ResponsePayloads;

public record SearchProfileItem(
    string? ProfileId,
    string? LastName,
    string? OtherNames,
    string? PassportNumber,
    string? Salutation);