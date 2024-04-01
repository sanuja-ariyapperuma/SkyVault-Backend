namespace SkyVault.Payloads.ResponsePayloads;

public sealed record WelcomeUserResponse(
    string? Upn,
    string FullName,
    string LastLogin,
    string? Role,
    string? Email);