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
    public IEnumerable<SkyVaultMenuItem>? Menus { get; private protected set; }
    public string? WebCorrelationId { get; private protected set; }
    
    protected void Init()
    {
        ArgumentNullException.ThrowIfNull(antiForgery);
            
        AntiForgeryToken = antiForgery.GetAndStoreTokens(HttpContext).RequestToken;
        
        GetSession();
        
        WebCorrelationId =
            (PageContext.HttpContext!
                .Items["X-Correlation-ID"] ??= "Not Available") as string;
    }

    private void GetSession()
    {
        Upn = HttpContext.Session.GetString("Session.Upn");
        FullName = HttpContext.Session.GetString("Session.FullName");
        Email = HttpContext.Session.GetString("Session.Email");
        Role = HttpContext.Session.GetString("Session.Role");
        
        var menusJson = HttpContext.Session.GetString("Session.Menus");
        if (menusJson != null)
        {
            Menus = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<SkyVaultMenuItem>>(menusJson);
        }
    }

    protected void SetSession()
    {
        HttpContext.Session.SetString("Session.Upn", Upn!);
        HttpContext.Session.SetString("Session.FullName", FullName!);
        HttpContext.Session.SetString("Session.Email", Email!);
        HttpContext.Session.SetString("Session.Role", Role!);

        if (Menus == null) return;
        
        var menusJson = System.Text.Json.JsonSerializer.Serialize(Menus);
        HttpContext.Session.SetString("Session.Menus", menusJson);
    }
}