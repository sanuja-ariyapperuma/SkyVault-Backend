using System.Text;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
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

        public ProfileDefinitionResponse? ProfileDefinitions { get; set; }

        public IActionResult OnGet([FromQuery] string? id)
        {
            base.Init();

            var profileDefinitionResponse = customerProxy.GetProfileDefinitionData();

            if(profileDefinitionResponse is {Succeeded:true})
            {
                TempData["ProfDef.Gender"] = profileDefinitionResponse.Value?.Genders;
                TempData["ProfDef.Nationality"] = profileDefinitionResponse.Value?.Nationalities;
                TempData["ProfDef.Salutations"] = profileDefinitionResponse.Value?.Salutations;
                TempData["ProfDef.Country"] = profileDefinitionResponse.Value?.Countries;
            }
            

            if (id == null) return Page();

            var skyResult = customerProxy.GetCustomerProfile(
                new GetProfileRequest(id, base.SystemUserId));

            if (skyResult is { Succeeded: true })
            {
                TempData["CustomerProfile.ID"] = id;
                TempData["CustomerProfile.PassportList"] = skyResult.Value?.Passports;
                TempData["CustomerProfile.VisaList"] = skyResult.Value?.Visas;

                return Page();
            }

            TempData["Message"] = skyResult?.Message;
            TempData["ErrorCode"] = skyResult?.ErrorCode;
            TempData["CorrelationId1"] = base.WebCorrelationId;
            TempData["CorrelationId2"] = skyResult?.CorrelationId;

            return RedirectPermanent("/error");
        }

        public IActionResult OnGetAddPassport([FromHeader(Name = "CUSTOMER_ID")] string? id)
        {
            base.Init();

            TempData["CustomerProfile.ID"] = id;

            return Partial("_AddPassport");
        }

        public IActionResult OnGetPassport([FromHeader(Name = "CUSTOMER_ID")] string? id,
            [FromHeader(Name = "PASSPORT_NO")] string passportNo)
        {
            base.Init();

            if (id == null) return Partial("_Passport");

            var result = customerProxy.GetPassport(new GetPassportRequest(id, base.SystemUserId, passportNo));

            if (!result.Succeeded) return Partial("_Passport", this);

            var passport = result.Value;

            Passport = new PassportModel(passport?.Id, passport?.LastName, passport?.OtherNames,
                passport?.PassportNumber, passport?.Gender, passport?.DateOfBirth, passport?.ExpiryDate,
                passport?.PlaceOfBirth, passport?.NationalityId, passport?.IsPrimary, passport?.CountryId);

            return Partial("_Passport", this);
        }

        public IActionResult OnGetVisa([FromHeader(Name = "CUSTOMER_ID")] string? id,
            [FromHeader(Name = "VISA_NO")] string visaNo)
        {
            base.Init();

            if (id == null) return Partial("_Visa");

            var result = customerProxy.GetVisa(new GetVisaRequest(id, base.SystemUserId));

            if (!result.Succeeded) return Partial("_Visa", this);

            var visa = result.Value;

            Visa = new VisaModel(visa?.Id, visa?.VisaNumber, visa?.IssuedPlace, visa?.IssuedDate, visa?.ExpireDate,
                visa?.CountryId, visa?.PassportNumber);

            return Partial("_Visa", this);
        }

        public IActionResult OnGetPassportList([FromHeader(Name = "CUSTOMER_ID")] string? id)
        {
            base.Init();

            if (id == null) return Partial("_PassportList");

            var skyResult = customerProxy.GetCustomerProfile(
                new GetProfileRequest(id, base.SystemUserId));

            if (skyResult is not { Succeeded: true }) return Partial("_PassportList");

            TempData["CustomerProfile.ID"] = id;
            TempData["CustomerProfile.PassportList"] = skyResult.Value?.Passports;

            return Partial("_PassportList");
        }

        public IActionResult OnGetVisaList([FromHeader(Name = "CUSTOMER_ID")] string? id)
        {
            base.Init();

            if (id == null) return Partial("_VisaList");

            var skyResult = customerProxy.GetCustomerProfile(
                new GetProfileRequest(id, base.SystemUserId));

            if (skyResult is not { Succeeded: true }) return Partial("_VisaList");

            TempData["CustomerProfile.ID"] = id;
            TempData["CustomerProfile.VisaList"] = skyResult.Value?.Visas;

            return Partial("_VisaList");
        }

        public void OnPostAddPassport()
        {
            base.Init();

            var form = Request.ReadFormAsync().Result;
            var fieldCustomerId = form["hdnCustomerId"];
            var fieldSalutation = form["addSalutation"];
            var fieldLastName = form["addLastName"];
            var fieldOtherNames = form["addOtherNames"];
            var fieldNationality = form["addNationality"];
            var fieldGender = form["addGender"];
            var fieldPlaceOfBirth = form["addPlaceOfBirth"];
            var fieldPassportNo = form["addPassportNo"];
            var fieldDateOfBirth = form["addDateOfBirth"];
            var fieldPassportExpiry = form["addPassportExpiry"];
            var fieldCountry = form["addCountry"];

            var validationMessage = new StringBuilder();

            if (fieldSalutation[0] == "0")
                validationMessage.Append("Salutation has not been selected.");
            if (fieldLastName[0] == string.Empty)
                validationMessage.Append("Last Name has not been specified. <br/>");
            if (fieldOtherNames[0] == string.Empty)
                validationMessage.Append("Other Names has not been specified.");
            if (fieldNationality[0] == "0")
                validationMessage.Append("Nationality has not been selected.");
            if (fieldGender[0] == "0")
                validationMessage.Append("Gender has not been selected.");
            if (fieldPlaceOfBirth[0] == string.Empty)
                validationMessage.Append("Place of Birth has not been specified.");
            if (fieldPassportNo[0] == string.Empty)
                validationMessage.Append("Passport has not been specified.");
            if (fieldDateOfBirth[0] == string.Empty)
                validationMessage.Append("Date of Birth has not been specified.");
            if (fieldPassportExpiry[0] == string.Empty)
                validationMessage.Append("Passport Expiry has not been specified.");
            if (fieldCountry[0] == "0")
                validationMessage.Append("Country has not been selected.");

            if (validationMessage.Length > 0)
            {
                Response.Headers["HX-Trigger"] = "{\"validationErrors\":\"" + validationMessage.ToString() + "\"}";
                return;
            }

            var passportData = new PassportRequest(string.Empty, fieldCustomerId, base.SystemUserId, string.Empty,
                fieldLastName, fieldOtherNames, fieldPassportNo, fieldGender, fieldDateOfBirth, fieldPlaceOfBirth,
                fieldPassportExpiry, fieldNationality, fieldCountry, string.Empty, fieldSalutation);

            var result = customerProxy.SavePassport(passportData);

            if (!result.Succeeded)
            {
                Response.Headers["HX-Trigger"] = "{\"validationErrors\":\"" + result.Message + "\"}";
            }
            else
            {
                Response.Headers["HX-Trigger"] = "refreshPassportList";
            }
        }
    
        
    }
}