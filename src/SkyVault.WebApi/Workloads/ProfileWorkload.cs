using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads
{
    internal static class ProfileWorkload
    {
        public static IResult SaveProfile(
            [FromBody] ProfilePayload profile,
            SkyvaultContext dbContext
        )
        {
            var result = new SkyResult<ProfilePayload>();
            var commonData = new CommonData(dbContext);
            var systemUserData = new SystemUserData(dbContext);

            #region Profile Validations 
            if (!int.TryParse(profile.SystemUserId, out int systemUserId))
                return Results.BadRequest(result
                    .Fail("Invalid SystemId", "400", "0"));

            var systemUser = systemUserData.GetUserByProfileId(systemUserId);

            if (systemUser == null)
                return Results.BadRequest(result
                    .Fail("No Matching System User Found", "400", "0"));

            if (!int.TryParse(profile.SalutationId, out int salutationId))
                return Results.BadRequest(result
                    .Fail("Invalid SalutationId", "400", "0"));

            var salutation = commonData.Salutation(salutationId);

            if (salutation == null)
                return Results.BadRequest(result
                    .Fail("No Matching Salutation Found", "400", "0"));

            if (!int.TryParse(profile.PreffdComMth, out int preffComMthId))
                return Results.BadRequest(result
                    .Fail("Invalid PreffComMthId", "400", "0"));

            var comMethod = commonData.GetCommunicationMethod(preffComMthId);

            if (comMethod == null)
                return Results.BadRequest(result
                    .Fail("No Matching Communication Method Found", "400", "0"));

            #endregion

            var passports = new List<Backend.Models.Passport>();

            var frequentFlyerNumbers = profile.FrequentFlyerNumbers
                .Select(item => new FrequentFlyerNumber() { FlyerNumber = item }).ToList();

            foreach (var passport in profile.Passports)
            {

                #region Passport Validations
                if (!int.TryParse(passport.CountryId, out int countryId))
                    return Results.BadRequest(result
                        .Fail("Invalid Country Id", "400", "0"));

                var country = commonData.GetCountry(countryId);
                if (country == null)
                    return Results.BadRequest(result
                        .Fail("No Matching Country Found", "400", "0"));

                if (!int.TryParse(passport.NationalityId, out int nationalityId))
                    return Results.BadRequest(result
                        .Fail("Invalid National", "400", "0"));

                var nationality = commonData.GetNationality(nationalityId);
                if (nationality == null)
                    return Results.BadRequest(result
                        .Fail("No Matching National", "400", "0"));
                #endregion

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
                    #region Visa Validations
                    if (!int.TryParse(visa.CountryId, out int visaCountryId))
                        return Results.BadRequest(result
                            .Fail("Invalid Visa Country Id", "400", "0"));

                    var visaCountry = commonData.GetCountry(visaCountryId);

                    if (visaCountry == null)
                        return Results.BadRequest(result
                            .Fail("No Matching Visa Country Found", "400", "0"));
                    #endregion

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
                    return Results.BadRequest(result
                        .Fail("No Matching Parent Profile Found", "400", "0"));

            }

            var newProfile = new CustomerProfile()
            {
                Salutation = salutation,
                Passports = passports,
                FrequentFlyerNumbers = frequentFlyerNumbers,
                SystemUser = systemUser,
                Parent = parent,
                PreferredComm = comMethod
            };

            var customerProfileData = new CustomerProfileData(dbContext);
            customerProfileData.Create(newProfile);

            return Results.Ok(result
                .SucceededWithValue(ToProfilePayload(newProfile)));
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
