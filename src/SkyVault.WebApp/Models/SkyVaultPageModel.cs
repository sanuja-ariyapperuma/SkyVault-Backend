using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkyVault.WebApp.Models;

public class SkyVaultPageModel(IAntiforgery antiForgery) : PageModel
{
    public string? AntiForgeryToken { get; private protected set; }
    public string? Upn { get; private protected set; }
    public string? FullName { get; private protected set; }
    public string? Email { get; private protected set; }
    public string? Role { get; private protected set; }
    
    public virtual void OnGet()
    {
        ArgumentNullException.ThrowIfNull(antiForgery);
            
        AntiForgeryToken = antiForgery.GetAndStoreTokens(HttpContext).RequestToken;
    }
}