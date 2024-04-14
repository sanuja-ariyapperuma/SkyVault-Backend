using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads
{
    internal static class ProfileWorkload
    {
        private static string _correlationId = string.Empty;

        public static IResult SaveProfile(
            [FromBody] ProfilePayload profile, SkyvaultContext dbContext, HttpContext context)
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var commonData = new CommonData(dbContext);
                var systemUserData = new SystemUserData(dbContext);

                var systemUser =
                    systemUserData.GetUserByProfileId(Convert.ToInt32(profile.SystemUserId), _correlationId);
                var salutation = commonData.Salutation(Convert.ToInt32(profile.SalutationId));
                var comMethod = commonData.GetCommunicationMethod(Convert.ToInt32(profile.PreffdComMth));

                if (!systemUser.Succeeded)
                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                            "No Matching System User Found", "30550615-0000", _correlationId)
                    );

                if (salutation == null)

                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                            "No Matching Salutation Found", "30550615-0001", _correlationId));

                if (comMethod == null)
                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                            "No Matching Communication Method Found", "30550615-0002", _correlationId));

                var passports = CreateNewPassport(profile, commonData);

                if (passports == null)
                    return Results.Problem(
                        new ValidationProblemDetails().ToValidationProblemDetails(
                            "Invalid Passport Or Visa Data Found", "30550615-0003", _correlationId));

                var frequentFlyerNumbers = profile.FrequentFlyerNumbers
                    .Select(item => new FrequentFlyerNumber() { FlyerNumber = item }).ToList();


                CustomerProfile? parent = null;

                if (int.TryParse(profile.ParentId, out int parentId))
                {
                    parent = dbContext.CustomerProfiles.Find(parentId);
                    if (parent == null)
                        return Results.Problem(
                            new ValidationProblemDetails().ToValidationProblemDetails(
                                "No Matching Parent Profile Found", "30550615-0004", _correlationId));
                }

                var newProfile = new CustomerProfile()
                {
                    Salutation = salutation,
                    Passports = passports,
                    FrequentFlyerNumbers = frequentFlyerNumbers,
                    SystemUser = systemUser.Value!,
                    Parent = parent,
                    PreferredComm = comMethod
                };

                var customerProfileData = new CustomerProfileData(dbContext);

                customerProfileData.Create(newProfile);

                return Results.Ok(ToProfilePayload(newProfile));
            }
            catch (FormatException e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "Invalid type of data found in the profile", "30550615-0005", _correlationId));
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.", "30550615-0006", _correlationId));
            }
        }

        public static IResult GetProfile(
            [FromRoute] string id,
            [FromRoute] string sysUserId,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var customerProfile = customerProfileData.Get(
                    Convert.ToInt32(id), Convert.ToInt32(sysUserId));

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
            [FromRoute] string SysUserId,
            [FromRoute] string RoleId,
            [FromRoute] string SearchQuery,
            SkyvaultContext dbContext,
            HttpContext context
        )
        {
            try
            {
                _correlationId = CorrelationHandler.Get(context);

                var customerProfileData = new CustomerProfileData(dbContext);

                var searchResult = customerProfileData.Search(
                    SearchQuery, Convert.ToInt32(SysUserId), Convert.ToInt32(RoleId));

                if (searchResult.Count == 0)
                    return Results.Problem(new ProblemDetails().ToProblemDetails(
                        "No profile found", "30550615-0010", _correlationId));

                var response = ToSearchProfileResponse(searchResult);

                return Results.Ok(response);
            }
            catch (Exception e)
            {
                e.LogException(_correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.",
                    "30550615-0011", _correlationId));
            }
        }

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

        private static List<SearchProfileResponse> ToSearchProfileResponse(
            List<CustomerProfile> customerProfile)
        {
            return customerProfile.SelectMany(cp => cp.Passports
                .Select(
                    p => new SearchProfileResponse(
                        cp.Id,
                        p.LastName,
                        p.OtherNames,
                        p.PassportNumber,
                        cp.Salutation.SalutationName
                    )
                )
            ).ToList();
        }

        private static List<Backend.Models.Passport> CreateNewPassport(ProfilePayload profile,
            CommonData commonData)
        {
            var passports = new List<Backend.Models.Passport>();

            foreach (var passport in profile.Passports)
            {
                var country = commonData.GetCountry(Convert.ToInt32(passport.CountryId));
                var nationality = commonData.GetNationality(Convert.ToInt32(passport.CountryId));

                if (country == null || nationality == null)
                    return null;

                List<Backend.Models.Visa> visas = [];

                if (passport.Visa.Length > 0)
                {
                    visas = CreateVisa(passport.Visa.ToList(), commonData);
                    if (visas == null)
                        return null;
                }


                var newpassport = new Backend.Models.Passport()
                {
                    LastName = passport.LastName,
                    OtherNames = passport.OtherNames,
                    PassportNumber = passport.PassportNumber,
                    Country = country,
                    Nationality = nationality,
                    Gender = passport.Gender,
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Parse(passport.DateOfBirth)),
                    PlaceOfBirth = passport.PlaceOfBirth,
                    IsPrimary = passport.IsPrimary,
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Parse(passport.ExpiryDate)),
                    Visas = visas
                };

                passports.Add(newpassport);
            }

            return passports;
        }

        private static List<Backend.Models.Visa> CreateVisa(
            List<Payloads.CommonPayloads.Visa> visas, CommonData commonData)
        {
            var visasEntity = new List<Backend.Models.Visa>();

            foreach (var visa in visas)
            {
                var visaCountry = commonData.GetCountry(Convert.ToInt32(visa.CountryId));

                if (visaCountry == null)
                    return null;

                var newVisa = new Backend.Models.Visa()
                {
                    VisaNumber = visa.VisaNumber,
                    Country = visaCountry,
                    IssuedPlace = visa.IssuedPlace,
                    IssuedDate = DateOnly.FromDateTime(DateTime.Parse(visa.IssuedDate)),
                    ExpireDate = DateOnly.FromDateTime(DateTime.Parse(visa.ExpireDate))
                };

                visasEntity.Add(newVisa);
            }

            return visasEntity;
        }
    }
}