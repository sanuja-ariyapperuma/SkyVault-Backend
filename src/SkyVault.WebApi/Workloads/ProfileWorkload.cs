using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads
{
    internal static class ProfileWorkload
    {
        private static string _correlationId = string.Empty;

        #region Workloads

        public static IResult GetProfile(
            [FromBody] GetProfileRequest getProfile,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var customerProfile = customerProfileData.Get(
                    Convert.ToInt32(getProfile?.id), Convert.ToInt32(getProfile?.sysUserId));

                if (customerProfile == null)
                    return Results.Problem(
                        new ProblemDetails().ToProblemDetails(
                            "No profile found", "30550615-0007", _correlationId));

                return Results.Ok(ToProfilePayload(customerProfile));
            }
            catch (FormatException e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "Invalid type of data in request", "30550615-0008", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.", "30550615-0009", _correlationId));
            }
        }

        public static IResult SearchProfiles(
            [FromBody] SearchProfileRequest searchProfileRequest,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchProfileRequest.SearchQuery))
                    return Results.Problem(new ValidationProblemDetails().ToValidationProblemDetails(
                                               "Empty search query found", "30550615-0011", _correlationId));

                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var customerProfiles = customerProfileData.Search(
                    searchProfileRequest.SearchQuery, 
                    Convert.ToInt32(searchProfileRequest.SysUserId));
                
                var searchedProfiles = ToSearchProfileResponse(customerProfiles!);
                var response = new SearchProfileResponse(
                    searchProfileRequest.SearchQuery,
                    searchedProfiles);

                return Results.Ok(response);
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.",
                    "30550615-0012", _correlationId));
            }
        }

        public static IResult AddPassport([FromBody] PassportRequest passportRequest,
            SkyvaultContext dbContext,
            HttpContext context) 
        {
            _correlationId = CorrelationHandler.Get(context);

            var customerProfileData = new CustomerProfileData(dbContext);

            var validateProfileData = customerProfileData.ValidateProfile(passportRequest, _correlationId);

            if (!validateProfileData.Succeeded)
                return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                        validateProfileData.Message, validateProfileData.ErrorCode, 
                                        _correlationId));

            if (String.IsNullOrWhiteSpace(passportRequest.CustomerProfileId)) 
            {
                var savedprofile = customerProfileData.SaveProfile(passportRequest, _correlationId);

                if (!savedprofile.Succeeded)
                    return Results.Problem(
                                           new ValidationProblemDetails().ToValidationProblemDetails(
                                                savedprofile.Message, savedprofile.ErrorCode, _correlationId));
            }
            else 
            {
                var passportData = new PassportData(dbContext);
                var savepassport = passportData.AddNewPassport(passportRequest, _correlationId);
            }


            

            return Results.Ok("Passport Added Successfully");

        }

        public static IResult UpdatePassport(
                [FromBody] PassportRequest passportRequest,
                SkyvaultContext dbContext,
                HttpContext context) 
        {
            _correlationId = CorrelationHandler.Get(context);
            
            var passportData = new PassportData(dbContext);

            var validatePassport = passportData.ValidatePassportDetails(passportRequest, _correlationId);

            if (!validatePassport.Succeeded)
                return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                            validatePassport.Message, validatePassport.ErrorCode, _correlationId));


            var result = passportData.UpdatePassport(passportRequest, _correlationId);

            if (!result.Succeeded)
                return Results.Problem(
                    new ValidationProblemDetails().ToValidationProblemDetails(
                    result.Message, result.ErrorCode, _correlationId));

            return Results.Ok(result.Value);
 
        }

        public static IResult AddVisa([FromBody] VisaReqeust visaReqeust,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            _correlationId = CorrelationHandler.Get(context);

            var visaData = new VisaData(dbContext);

            var validateVisa = visaData.ValidateVisaDetails(visaReqeust, _correlationId);

            if (!validateVisa.Succeeded)
                return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                        validateVisa.Message, validateVisa.ErrorCode, _correlationId));

            var result = visaData.AddVisa(visaReqeust, _correlationId);

            if (!result.Succeeded)
                return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                        result.Message, result.ErrorCode, _correlationId));

            return Results.Ok(result.Value);
        }

        public static IResult UpdateVisa([FromBody] VisaReqeust visaReqeust,
            SkyvaultContext dbContext,
            HttpContext context)
        {
            _correlationId = CorrelationHandler.Get(context);

            var visaData = new VisaData(dbContext);

            var validateVisa = visaData.ValidateVisaDetails(visaReqeust, _correlationId);

            if (!validateVisa.Succeeded)
                return Results.Problem(
                                    new ValidationProblemDetails().ToValidationProblemDetails(
                                    validateVisa.Message, validateVisa.ErrorCode, _correlationId));

            var result = visaData.UpdateVisa(visaReqeust, _correlationId);

            if (!result.Succeeded)
                return Results.Problem(
                    new ValidationProblemDetails().ToValidationProblemDetails(
                    result.Message, result.ErrorCode, _correlationId));

            return Results.Ok(result.Value);
        }

        public static IResult UpdateComMethod([FromBody] ComMethodRequest comMethodRequest,
                       SkyvaultContext dbContext,
                                  HttpContext context)
        {
            _correlationId = CorrelationHandler.Get(context);

            var customerProfileData = new CustomerProfileData(dbContext);

            var result = customerProfileData.UpdateComMethod(comMethodRequest, _correlationId);

            if (!result.Succeeded)
                return Results.Problem(
                                       new ValidationProblemDetails().ToValidationProblemDetails(
                                                              result.Message, result.ErrorCode, _correlationId));

            return Results.Ok(result.Value);
        }

        #endregion

        #region Private Methods

        private static ProfilePayload ToProfilePayload(CustomerProfile customerProfile)
        {
            var passports = customerProfile.Passports.Select(passport =>
            {
                var visas = passport.Visas.Select(visa => new Payloads.CommonPayloads.Visa(
                    visa.Id.ToString(),
                    visa.VisaNumber,
                    visa.CountryId.ToString(),
                    visa.IssuedPlace,
                    visa.IssuedDate.ToShortDateString(),
                    visa.ExpireDate.ToShortDateString(),
                    ""
                )).ToList();

                return new Payloads.CommonPayloads.Passport(
                    passport.Id.ToString(),
                    passport.LastName,
                    passport.OtherNames,
                    passport.PassportNumber,
                    passport.Gender,
                    passport.DateOfBirth.ToShortDateString(),
                    passport.PlaceOfBirth,
                    passport.ExpiryDate?.ToShortDateString(),
                    passport.NationalityId.ToString(),
                    passport.CountryId.ToString(),
                    passport.IsPrimary,
                    visas.ToArray()
                );
            }).ToList();

            var profilePayload = new ProfilePayload(
                customerProfile.Id.ToString(),
                customerProfile.SalutationId.ToString(),
                passports.ToArray(),
                customerProfile.FrequentFlyerNumbers.Select(item => item.FlyerNumber).ToArray(),
                customerProfile.Id.ToString(),
                customerProfile.ParentId.ToString(),
                customerProfile.SystemUserId.ToString()
            );

            return profilePayload;
        }

        private static IEnumerable<SearchProfileItem> ToSearchProfileResponse(
            IEnumerable<CustomerProfile> customerProfile)
        {
            return customerProfile.SelectMany(cp => cp.Passports
                .Select(
                    p => new SearchProfileItem(
                        cp.Id,
                        p.LastName,
                        p.OtherNames,
                        p.PassportNumber,
                        cp.Salutation.SalutationName
                    )
                )
            ).ToList();
        }

        #endregion
        
    }
}