namespace SkyVault.WebApp.Models;

public sealed record SkyVaultUser(
    string? Upn, 
    string? FirstName, 
    string? LastName, 
    string? Email,
    string? Role);