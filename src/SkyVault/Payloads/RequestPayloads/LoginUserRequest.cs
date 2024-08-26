namespace SkyVault.Payloads.RequestPayloads;

//Information to be ideally populated through the claims
public sealed record LoginUserRequest(
    string? Upn, 
    string? UserRole);