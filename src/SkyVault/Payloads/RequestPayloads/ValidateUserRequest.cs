namespace SkyVault.Payloads.RequestPayloads;

//Information to be ideally populated through the claims
public sealed record ValidateUserRequest(
    string Username, 
    string FirstName,
    string LastName,
    string Email);