namespace SkyVault.WebApp.Models;

internal sealed record SkyVaultUser(
    string? Upn, 
    string? FirstName, 
    string? LastName, 
    string? Email);