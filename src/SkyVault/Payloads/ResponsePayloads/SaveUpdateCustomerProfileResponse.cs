namespace SkyVault.Payloads.ResponsePayloads
{
    public record SaveUpdateCustomerProfileResponse(
        string? CustomerProfileId,
        string? PassportId = ""
    );
        
}