using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApp.Models;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    //[Authorize]
    public class IndexModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy,
        CustomerProxy customerProxy) : Models.SkyVaultPageModel(antiForgery)
    {
        /*public override void OnGet()
        {
            base.OnGet();

            if (User.Identity is not { IsAuthenticated: true })
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            //Making sure that all values required by the claims are present before proceeding.
            if (HttpContext.Session.GetString(StateKeys.ClaimsCheckSessionKey) is null)
                HttpContext.Response.Redirect("/claimspage", true);

            var claimDictionary = User.Claims.ToDictionary(c => c.Type, c => c.Value);

            var skyUser = new SkyVaultUser(
                claimDictionary.GetValueOrDefault(ClaimTypes.Email),
                claimDictionary.GetValueOrDefault(ClaimTypes.Name),
                claimDictionary.GetValueOrDefault(ClaimTypes.Surname),
                claimDictionary.GetValueOrDefault(ClaimTypes.Email),
                claimDictionary.GetValueOrDefault(SkyVaultConfigurationKeys.SkyVaultUserRoleClaimKey));

            var userRequest = new ValidateUserRequest(skyUser.Upn, skyUser.FirstName,
                skyUser.LastName, skyUser.Email, skyUser.Role);

            var skyResult = authorityProxy.GetUserInfo(userRequest);

            if (skyResult!.Succeeded == false)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            //Bind only what was processed by the API and not what was received by the claims.
            var welcomeUser = skyResult.Value;

            base.Upn = welcomeUser.Upn;
            base.FullName = welcomeUser.FullName;
            base.Email = welcomeUser.Email;
            base.Role = welcomeUser.Role;

            //Get menus as per the assigned role.
            ViewData["UserMenus"] = authorityProxy.GetMenus(Role);
        }*/

        public IActionResult OnGet()
        {
            base.Init();

            var skyUser = new SkyVaultUser(
                "test@gmail.com",
                "Test", 
                "User", 
                "test@gmail.com", 
                "Admin");

            var userRequest = new ValidateUserRequest(skyUser.Upn, skyUser.FirstName,
                skyUser.LastName, skyUser.Email, skyUser.Role);

            var skyResult = authorityProxy.GetUserInfo(userRequest);

            if (skyResult!.Succeeded == false)
            {
                TempData["Message"] = skyResult.Message;
                TempData["ErrorCode"] = skyResult.ErrorCode;
                TempData["CorrelationId1"] = WebCorrelationId;
                TempData["CorrelationId2"] = skyResult.CorrelationId;

                return RedirectToPage("/unauthorized");
            }

            //Bind only what was processed by the API and not what was received by the claims.
            var welcomeUser = skyResult.Value;

            base.Upn = welcomeUser?.Upn;
            base.FullName = welcomeUser?.FullName;
            base.Email = welcomeUser?.Email;
            base.Role = welcomeUser?.Role;
            base.Menus = authorityProxy.GetMenus(Role!);
            base.SetSession();

            return Page();
        }

        public RedirectResult OnGetSignOut()
        {
            var signOutUrl = $"https://login.microsoftonline.com/{configuration["AzureAD:TenantId"]}/oauth2/logout";

            return RedirectPermanent(signOutUrl);
        }

        public IActionResult OnPostSearch([FromForm] string searcher)
        {
            base.Init();

            var searchProfileRequest = new SearchProfileRequest(this.Upn, this.Role, searcher);
            var skyResult = customerProxy.SearchProfile(searchProfileRequest);

            if (skyResult!.Succeeded == false)
            {
                TempData["Message"] = skyResult.Message;
                TempData["ErrorCode"] = skyResult.ErrorCode;
                TempData["CorrelationId1"] = WebCorrelationId;
                TempData["CorrelationId2"] = skyResult.CorrelationId;

                return RedirectToPage("/innererror");
            }

            var searchProfileResponse = skyResult.Value;

            if (searchProfileResponse?.Profiles != null && searchProfileResponse.Profiles.Any())
            {
                var sb = new StringBuilder();

                foreach (var profile in searchProfileResponse.Profiles)
                {
                    var profileData = new List<string?>
                    {
                        profile.LastName,
                        profile.OtherNames,
                        profile.Salutation,
                        profile.PassportNumber
                    };

                    sb.Append($"<tr><td>{string.Join("</td><td>", profileData)}</td></tr>");
                }

                return Content(sb.ToString(), "text/html");
            }
            else
            {
                return Content("No results found", "text/html");
            }
        }
    }
}