using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.WebApp.Models;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    public class CustomerProfileModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        AuthorityProxy authorityProxy) : Models.SkyVaultPageModel(antiForgery)
    {
        public PassportModel? Passport { get; set; }
        public VisaModel? Visa { get; set; }
        
        public IActionResult OnGet()
        {
            base.Init();
            
            return Page();
        }

        public IActionResult OnPost([FromHeader] string customerId)
        {
            base.Init();
            
            //Assign the selected customerid to be pickedup by the roundtrip of the same page
            TempData["Selected.CustomerId"] = customerId;
            
            Response.Headers["HX-Redirect"] = "/customerprofile";

            return RedirectPermanent("customerprofile");
        }

        public IActionResult OnGetPassport(string passportNo)
        {
            return Partial("_Passport");
        }

        public IActionResult OnGetVisa(string visaNo)
        {
           return Partial("_Visa");
        }
    }
}