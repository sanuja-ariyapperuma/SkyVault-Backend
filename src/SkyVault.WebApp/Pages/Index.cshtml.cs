using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApp.Pages
{
    [Authorize]
    public class IndexModel(IConfiguration configuration, IAntiforgery antiForgery) 
        : Models.SkyVaultPageModel(antiForgery)
    {
        public override void OnGet()
        {
            base.OnGet();

            if (User.Identity is not { IsAuthenticated: true })
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            
            base.Username = "";
            base.FullName = "";
            base.Email = "";
        }

        public RedirectResult OnGetSignOut()
        {
            var signOutUrl = $"https://login.microsoftonline.com/{configuration["AzureAD:TenantId"]}/oauth2/logout";

            return RedirectPermanent(signOutUrl);
        }
    }
}
