using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkyVault.WebApp.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public static bool RedirectToSso = false;
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            //Set this value, so it will be picked up in the OnRedirectToIdentityProvider
            LoginModel.RedirectToSso = true;
            
            return Challenge(new AuthenticationProperties { RedirectUri = "/" });
        } 
            
    }
}
