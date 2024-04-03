using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads;
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

            var systemUserData = new SystemUserData(dbContext);

            var systemUser = systemUserData.GetUserByProfileId(Convert.ToInt32(profile.SystemUserId));

            if (systemUser == null)
            {
                return Results.BadRequest(result.Fail("No Matching System User Found", "400", "0"));
            }

            var commonData = new CommonData(dbContext);
            
            var salutation = commonData.Salutation(Convert.ToInt32(profile.SalutationId));

            if (salutation == null)
            {
                return Results.BadRequest(result.Fail("No Matching Salutation Found", "400", "0"));
            }

            var comMethod = commonData.GetCommunicationMethod(Convert.ToInt32(profile.PreffdComMth));

            if (comMethod == null)
            {
                return Results.BadRequest(result.Fail("No Matching Communication Method Found", "400", "0"));
            }

            var passports = new List<Backend.Models.Passport>();
            var frequentFlyerNumbers = profile.FrequentFlyerNumbers.Select(item => new FrequentFlyerNumber() { FlyerNumber = item }).ToList();

            foreach (var passport in profile.Passports) 
            {
                var country = commonData.GetCountry(Convert.ToInt32(passport.CountryId));
                if (country == null)
                {
                    return Results.BadRequest(result.Fail("No Matching Country Found", "400", "0"));
                }

                var nationality = commonData.GetNationality(Convert.ToInt32(passport.NationalityId));

                var newpassport = new Backend.Models.Passport()
                {
                    LastName = passport.LastName,
                    OtherNames = passport.OtherNames,
                    PassportNumber = passport.PassportNumber,
                    Country = country,
                    Nationality = nationality,
                    Gender = passport.Gender,
                    DateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(passport.DateOfBirth)),
                    PlaceOfBirth = passport.PlaceOfBirth,
                    IsPrimary = passport.IsPrimary,
                    ExpiryDate = DateOnly.FromDateTime(Convert.ToDateTime(passport.ExpiryDate)),
                };

                var visas = new List<Backend.Models.Visa>();

                foreach (var visa in passport.Visa)
                {
                    var visaCountry = commonData.GetCountry(Convert.ToInt32(visa.CountryId));

                    if (visaCountry == null)
                    {
                        return Results.BadRequest(result.Fail("No Matching Visa Country Found", "400", "0"));
                    }

                    var newVisa = new Backend.Models.Visa()
                    {
                        VisaNumber = visa.VisaNumber,
                        Country = visaCountry,
                        IssuedPlace = visa.IssuedPlace,
                        IssuedDate = DateOnly.FromDateTime(Convert.ToDateTime(visa.IssuedDate)),
                        ExpireDate = DateOnly.FromDateTime(Convert.ToDateTime(visa.ExpireDate))
                    };

                    visas.Add(newVisa);
                }
                newpassport.Visas = visas;
                passports.Add(newpassport);
            }

            Backend.Models.CustomerProfile parent = null;

            if (int.TryParse(profile.ParentId, out int parentId))
            {
                parent = dbContext.CustomerProfiles.Find(parentId);
                if (parent == null)
                {
                    return Results.BadRequest(result.Fail("No Matching Parent Profile Found", "400", "0"));
                }
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

            return Results.Ok(result.SucceededWithValue(ToProfilePayload(newProfile)));
        }

        public static IResult GetProfile(
                       [FromRoute] string id, 
                       [FromRoute] string sysUserId,
                       SkyvaultContext dbContext
                   )
        {

            ;
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

        //public static SkyResult<List<ProfileSearchResponse>> GetAllProfiles(
        //        [FromRoute] string SysUserId,
        //        [FromRoute] string RoleId,
        //        [FromRoute] string SearchQuery,
        //        SkyvaultContext dbContext
        //    ) 
        //{
        //    var searchResult = dbContext.CustomerProfiles.GetAllProfiles(SysUserId, RoleId, SearchQuery, dbContext);
            
        //    var result = new SkyResult<List<ProfileSearchResponse>>();

        //    return result.SucceededWithValue(searchResult);
        //}

        private static ProfilePayload ToProfilePayload(CustomerProfile customerProfile)
        {
            var passports = new List<Payloads.Passport>();

            foreach (var passport in customerProfile.Passports)
            {
                var visas = new List<Payloads.Visa>();

                foreach (var visa in passport.Visas)
                {
                    var v = new Payloads.Visa(
                        visa.Id.ToString(),
                        visa.VisaNumber,
                        visa.CountryId.ToString(),
                        visa.IssuedPlace,
                        visa.IssuedDate.ToShortDateString(),
                        visa.ExpireDate.ToShortDateString(),
                        ""
                        );
                    visas.Add(v);
                }

                var psp = new Payloads.Passport(
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
                passports.Add(psp);

            }

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

    }
}
