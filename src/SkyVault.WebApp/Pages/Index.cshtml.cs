using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    [Authorize]
    internal class IndexModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy) : Models.SkyVaultPageModel(antiForgery)
    {
        public override void OnGet()
        {
            base.OnGet();

            if (User.Identity is not { IsAuthenticated: true })
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            //Get user information from database
            var userRequest = new ValidateUserRequest("ayub@hsenidbiz.com", "Ayub",
                "Sourjah", "ayub@hsenidbiz.com");

            var welcomeUser = authorityProxy.GetUserInfo(userRequest).Result ?? 
                              throw new ArgumentNullException("authorityProxy.GetUserInfo(userRequest).Result");

            base.Username = welcomeUser.Username;
            base.FullName = welcomeUser.FullName;
            base.Email = welcomeUser.Email;
        }

        public RedirectResult OnGetSignOut()
        {
            var signOutUrl = $"https://login.microsoftonline.com/{configuration["AzureAD:TenantId"]}/oauth2/logout";

            return RedirectPermanent(signOutUrl);
        }
    }
}