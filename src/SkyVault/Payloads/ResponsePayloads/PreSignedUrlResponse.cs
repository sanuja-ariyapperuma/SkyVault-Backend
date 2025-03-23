namespace SkyVault.Payloads.ResponsePayloads
{
    public record PreSignedUrlResponse(string signedUrl, string randomFileName);
}
