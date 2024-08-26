namespace SkyVault.Payloads.ResponsePayloads;

public sealed record WelcomeUserResponse(
    string FullName,
    string UserRole);