namespace SkyVault.Payloads.ResponsePayloads;

public sealed record WelcomeUserResponse(
    string Username,
    string FullName,
    string LastLogin,
    string Role,
    string Email);