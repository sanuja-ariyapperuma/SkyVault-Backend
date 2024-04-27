using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    public class CustomerProfileModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy) : Models.SkyVaultPageModel(antiForgery)
    {
        public IActionResult OnGet()
        {
            base.Init();

            return Page();
        }

        public IActionResult OnGetPassport(string passportNo)
        {
            return Partial("_Passport");
        }

        public IActionResult OnGetVisa(string visaNo)
        {
            return Partial("_Vista");
        }
    }
}