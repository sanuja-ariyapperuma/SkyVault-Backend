using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApp.Models;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    //[Authorize]
    public class IndexModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy) : Models.SkyVaultPageModel(antiForgery)
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
        
        public override IActionResult OnGet()
        {
            base.OnGet();
            
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
                return RedirectToPage("/unauthorized");
            }
            
            //Bind only what was processed by the API and not what was received by the claims.
            var welcomeUser = skyResult.Value;
            
            base.Upn = welcomeUser.Upn;
            base.FullName = welcomeUser.FullName;
            base.Email = welcomeUser.Email;
            base.Role = welcomeUser.Role;
            
            //Get menus as per the assigned role.
            ViewData["UserMenus"] = authorityProxy.GetMenus(Role!);
            
            return Page();
        }

        public RedirectResult OnGetSignOut()
        {
            var signOutUrl = $"https://login.microsoftonline.com/{configuration["AzureAD:TenantId"]}/oauth2/logout";

            return RedirectPermanent(signOutUrl);
        }
    }
}