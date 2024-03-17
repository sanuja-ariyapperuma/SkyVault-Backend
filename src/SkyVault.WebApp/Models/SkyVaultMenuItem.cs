namespace SkyVault.WebApp.Models;

public sealed record SkyVaultMenuItem(
    string? MenuName, 
    string? MenuIconUri, 
    string? MenuUri,
    string? Role,
    IEnumerable<SkyVaultMenuItem>? Children = null);