namespace SkyVault.Payloads.RequestPayloads;

public record ComMethodUpdateRequest(
    string? CustomerProfileId,
    string? EmailAddress,
    string? PrefCommId,
    string? WhatsAppNumber
    );
