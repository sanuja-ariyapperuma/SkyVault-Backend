using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.WebApp.Models;

namespace SkyVault.WebApp.Pages
{
    public class CreateProfileModel(IAntiforgery antiForgery) : SkyVaultPageModel(antiForgery)
    {
        public void OnGet()
        {
            this.Init();
        }
    }
}
