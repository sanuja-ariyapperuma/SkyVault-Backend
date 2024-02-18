namespace SkyVault.Payloads.RequestPayloads;

public sealed record ValidateUserRequest(
    string Username,
    string FirstName,
    string LastName,
    string Email);