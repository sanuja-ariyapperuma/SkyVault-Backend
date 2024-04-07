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
        public static IResult SaveProfile(
            [FromBody] ProfilePayload profile, SkyvaultContext dbContext, HttpContext context)
        {
            var correlationId = context.Items["X-Correlation-ID"]?.ToString();

            var commonData = new CommonData(dbContext);
            var systemUserData = new SystemUserData(dbContext);

            ValidationProblemDetails BuildValidationProblemDetails(string detail, string errorCode) =>
                new ValidationProblemDetails().ToValidationProblemDetails(detail, errorCode, correlationId);

            try
            {
                if (!int.TryParse(profile.SystemUserId, out int systemUserId))
                    return Results.Problem(BuildValidationProblemDetails("Invalid System User Id", "30550615-0000"));

                var systemUser = systemUserData.GetUserByProfileId(systemUserId, correlationId);

                if (!systemUser.Succeeded)
                    return Results.Problem(BuildValidationProblemDetails("No Matching System User Found",
                        "30550615-0001"));

                if (!int.TryParse(profile.SalutationId, out int salutationId))
                    return Results.Problem(BuildValidationProblemDetails("Invalid Salutation Id", "30550615-0002"));

                var salutation = commonData.Salutation(salutationId);

                if (salutation == null)
                    return Results.Problem(BuildValidationProblemDetails("No Matching Salutation Found",
                        "30550615-0003"));

                if (!int.TryParse(profile.PreffdComMth, out int preffComMthId))
                    return Results.Problem(BuildValidationProblemDetails("Invalid Communication Method Id",
                        "30550615-0004"));

                var comMethod = commonData.GetCommunicationMethod(preffComMthId);

                if (comMethod == null)
                    return Results.Problem(BuildValidationProblemDetails("No Matching Communication Method Found",
                        "30550615-0005"));

                var passports = new List<Backend.Models.Passport>();

                var frequentFlyerNumbers = profile.FrequentFlyerNumbers
                    .Select(item => new FrequentFlyerNumber() { FlyerNumber = item }).ToList();

                foreach (var passport in profile.Passports)
                {
                    if (!int.TryParse(passport.CountryId, out int countryId))
                        return Results.Problem(BuildValidationProblemDetails("Invalid Country Id", "30550615-0006"));

                    var country = commonData.GetCountry(countryId);
                    if (country == null)
                        return Results.Problem(BuildValidationProblemDetails("No Matching Country Found",
                            "30550615-0007"));

                    if (!int.TryParse(passport.NationalityId, out int nationalityId))
                        return Results.Problem(BuildValidationProblemDetails("Invalid Nationality Id",
                            "30550615-0008"));

                    var nationality = commonData.GetNationality(nationalityId);
                    if (nationality == null)
                        return Results.Problem(BuildValidationProblemDetails("No Matching Nationality Found",
                            "30550615-0009"));

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
                    };

                    var visas = new List<Backend.Models.Visa>();

                    foreach (var visa in passport.Visa)
                    {
                        if (!int.TryParse(visa.CountryId, out int visaCountryId))
                            return Results.Problem(BuildValidationProblemDetails("Invalid Visa Country Id",
                                "30550615-0010"));

                        var visaCountry = commonData.GetCountry(visaCountryId);

                        if (visaCountry == null)
                            return Results.Problem(BuildValidationProblemDetails("No Matching Visa Country Found",
                                "30550615-0011"));

                        var newVisa = new Backend.Models.Visa()
                        {
                            VisaNumber = visa.VisaNumber,
                            Country = visaCountry,
                            IssuedPlace = visa.IssuedPlace,
                            IssuedDate = DateOnly.FromDateTime(DateTime.Parse(visa.IssuedDate)),
                            ExpireDate = DateOnly.FromDateTime(DateTime.Parse(visa.ExpireDate))
                        };

                        visas.Add(newVisa);
                    }

                    newpassport.Visas = visas;
                    passports.Add(newpassport);
                }

                CustomerProfile? parent = null;

                if (int.TryParse(profile.ParentId, out int parentId))
                {
                    parent = dbContext.CustomerProfiles.Find(parentId);
                    if (parent == null)
                        return Results.Problem(BuildValidationProblemDetails("No Matching Parent Profile Found",
                            "30550615-0012"));
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
            catch (Exception e)
            {
                e.LogException(correlationId);

                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "An unexpected error occurred. Please try again later.", "30550615-0001", correlationId));
            }
        }

        public static IResult GetProfile(
            [FromRoute] string id,
            [FromRoute] string sysUserId,
            SkyvaultContext dbContext
        )
        {
            var customerProfileData = new CustomerProfileData(dbContext);

            if (!int.TryParse(id, out int profileId))
                return Results.BadRequest(new SkyResult<ProfilePayload>()
                    .Fail("Invalid profile id found", "400", "0"));

            if (!int.TryParse(sysUserId, out int systemUserId))
                return Results.BadRequest(new SkyResult<ProfilePayload>()
                    .Fail("Invalid system user id found", "400", "0"));

            var customerProfile = customerProfileData.Get(profileId, systemUserId);

            if (customerProfile == null)
                return Results.NotFound(new SkyResult<ProfilePayload>()
                    .Fail("No profile found", "404", "0"));

            return Results.Ok(new SkyResult<ProfilePayload>()
                .SucceededWithValue(ToProfilePayload(customerProfile)));
        }

        public static IResult SearchProfiles(
            [FromRoute] string SysUserId,
            [FromRoute] string RoleId,
            [FromRoute] string SearchQuery,
            SkyvaultContext dbContext
        )
        {
            var customerProfileData = new CustomerProfileData(dbContext);

            if (!int.TryParse(SysUserId, out int systemUserId))
                return Results.BadRequest(new SkyResult<ProfilePayload>()
                    .Fail("Invalid profile id found", "400", "0"));

            var searchResult = customerProfileData.Search(SearchQuery, systemUserId, Convert.ToInt32(RoleId));

            if (searchResult.Count == 0)
                return Results.NotFound(new SkyResult<ProfilePayload>()
                    .Fail("No profiles found", "404", "0"));

            var response = ToSearchProfileResponse(searchResult);

            var result = new SkyResult<List<SearchProfileResponse>>();

            return Results.Ok(result.SucceededWithValue(response));
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

        private static List<SearchProfileResponse> ToSearchProfileResponse(List<CustomerProfile> customerProfile)
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
    }
}