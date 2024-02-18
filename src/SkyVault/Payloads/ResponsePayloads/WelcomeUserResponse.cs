namespace SkyVault.Payloads.ResponsePayloads;

public sealed record WelcomeUserResponse(
    string Username,
    string LastLogin);