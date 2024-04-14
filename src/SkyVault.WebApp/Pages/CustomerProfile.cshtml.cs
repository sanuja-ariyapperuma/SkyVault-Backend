using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    public class CustomerProfileModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy,
        IHttpContextAccessor httpContextAccessor) : Models.SkyVaultPageModel(antiForgery)
    {
        public IActionResult OnGet()
        {
            base.Init();
            
            return Page();
        }
    }
}
