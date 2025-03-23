namespace SkyVault.Payloads.CommonPayloads
{
    public record AuthenticatedUser(
            string DisplayName,
            string UserRole,
            string AccessToken
        );
}
