namespace SkyVault.Payloads.CommonPayloads;

public record Nationality()
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

