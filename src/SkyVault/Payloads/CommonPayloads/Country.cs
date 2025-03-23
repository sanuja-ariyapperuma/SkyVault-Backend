namespace SkyVault.Payloads.CommonPayloads;

public record Country()
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CountryCode { get; set; } = string.Empty;

}

