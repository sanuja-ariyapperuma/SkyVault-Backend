using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApp.Models;
using SkyVault.WebApp.Proxies;

namespace SkyVault.WebApp.Pages
{
    public class CustomerProfileModel(
        IConfiguration configuration,
        IAntiforgery antiForgery,
        CustomerProxy customerProxy) : Models.SkyVaultPageModel(antiForgery)
    {
        public PassportModel? Passport { get; set; }
        public VisaModel? Visa { get; set; }
        
        public IActionResult OnGet([FromQuery] string? id)
        {
            base.Init();

            if (id == null) return Page();

            var skyResult = customerProxy.GetCustomerProfile(
                new GetProfileRequest(id, base.SystemUserId));

            if (skyResult is { Succeeded: true })
            {
                TempData["CustomerProfile.ID"] = id;
                TempData["CustomerProfile.PassportList"] = skyResult.Value?.Passports;
                
                return Page();
            }

            TempData["Message"] = skyResult?.Message;
            TempData["ErrorCode"] = skyResult?.ErrorCode;
            TempData["CorrelationId1"] = base.WebCorrelationId;
            TempData["CorrelationId2"] = skyResult?.CorrelationId;

            return RedirectPermanent("/error");
        }

        public IActionResult OnGetPassport(string passportNo)
        {
            return Partial("_Passport");
        }

        public IActionResult OnGetVisa(string visaNo)
        {
           return Partial("_Visa");
        }

        public IActionResult OnGetPassportList(string customerId)
        {
            return Partial("_PassportList");
        }
        
        public IActionResult OnGetVisaList(string customerId)
        {
            return Partial("_VisaList");
        }
    }
}