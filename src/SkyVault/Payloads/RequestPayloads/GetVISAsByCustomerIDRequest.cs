namespace SkyVault.Payloads.RequestPayloads
{
    public record GetVISAsByCustomerIDRequest(
        string? CustomerProfileId,
        string? SystemUserId
        );
}
