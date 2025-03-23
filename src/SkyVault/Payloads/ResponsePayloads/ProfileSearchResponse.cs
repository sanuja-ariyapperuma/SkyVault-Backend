namespace SkyVault.Payloads.ResponsePayloads
{
    public record ProfileSearchResponse(
        string? ProfileId,
        string? FullName,
        string? PassportNumber
    );

}
