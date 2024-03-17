using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.WebApp.Models;

namespace SkyVault.WebApp.Pages
{
    public class ClaimsPageModel() : PageModel
    {
        public void OnGet()
        {
            var claimDictionary = User.Claims.ToDictionary(c => c.Type, c => c.Value);

            SkyUser = new SkyVaultUser(
                claimDictionary.GetValueOrDefault(ClaimTypes.Upn),
                claimDictionary.GetValueOrDefault(ClaimTypes.Name),
                claimDictionary.GetValueOrDefault(ClaimTypes.Surname),
                claimDictionary.GetValueOrDefault(ClaimTypes.Email),
                claimDictionary.GetValueOrDefault(SkyVaultConfigurationKeys.SkyVaultUserRoleClaimKey));

            if (SkyUser.Upn == null || SkyUser.FirstName == null || SkyUser.LastName == null ||
                SkyUser.Email == null) return;

            HttpContext.Session.SetString(StateKeys.ClaimsCheckSessionKey, "true");
            HttpContext.Response.Redirect("index", true);
        }

        public SkyVaultUser? SkyUser { get; set; }
    }
}