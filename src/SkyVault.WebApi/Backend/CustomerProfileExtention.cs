using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public static class CustomerProfileExtention
    {
        public static async Task<ProfilePayload> GetProfile(
            this DbSet<CustomerProfile> customerProfile, 
            string profileId, 
            string systemUserId,
            SkyvaultContext db)
        {
            var profile = await db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefaultAsync(p => p.Id == Convert.ToInt32(profileId) && p.SystemUserId == Convert.ToInt32(systemUserId));

            if (profile == null)
            {
                return null;
            }

            return ToProfilePayload(profile);
        }

        public static async Task<ProfilePayload> CreateProfile(
            this DbSet<CustomerProfile> customerProfile, 
            ProfilePayload profile, 
            SkyvaultContext dbContext)
        {
            var systemUser = await dbContext.SystemUsers.FindAsync(Convert.ToInt32(profile.SystemUserId));
            var salutation = await dbContext.Salutations.FindAsync(Convert.ToInt32(profile.SalutationId));
            var comMethod = await dbContext.CommunicationMethods.FindAsync(Convert.ToInt32(profile.PreffdComMth));
            

            if (systemUser == null || salutation == null || comMethod == null)
            {
                return null;
            }

            var passports = new List<Models.Passport>();
            var frequentFlyerNumbers = profile.FrequentFlyerNumbers.Select(item => new FrequentFlyerNumber() { FlyerNumber = item }).ToList();

            foreach (var passport in profile.Passports)
            {
                var country = await dbContext.Countries.FindAsync(Convert.ToInt32(passport.CountryId));
                var nationality = await dbContext.Nationalities.FindAsync(Convert.ToInt32(passport.NationalityId));

                var newpassport = new Models.Passport() 
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

                var visas = new List<Models.Visa>();

                foreach (var visa in passport.Visa)
                {
                    var visaCountry = await dbContext.Countries.FindAsync(Convert.ToInt32(visa.CountryId));

                    var newVisa = new Models.Visa()
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

            Models.CustomerProfile parent = null;

            if(int.TryParse(profile.ParentId, out int parentId)) 
            {
                parent = await dbContext.CustomerProfiles.FindAsync(parentId);
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
            
            dbContext.CustomerProfiles.Add(newProfile);
            await dbContext.SaveChangesAsync();

            return ToProfilePayload(newProfile);
        }

        public static async Task<List<ProfileSearchResponse>> GetAllProfiles(
                this DbSet<CustomerProfile> customerProfile,
                string sysUserId,
                string roleId,
                string searchQuery,
                SkyvaultContext dbContext
            ) 
        {
            var customerProfiles = await dbContext.CustomerProfiles.Include(p => p.Passports).Where(profile => 
            profile.SystemUserId == Convert.ToInt32(sysUserId)
            &&
            profile.Passports.Any(passport =>
                passport.LastName.ToLower().Contains(searchQuery.ToLower()) ||
                passport.OtherNames.ToLower().Contains(searchQuery.ToLower()) ||
                passport.PassportNumber.Contains(searchQuery)
            )
            ).ToListAsync();

            var resultsProfiles = new List<ProfileSearchResponse>();

            foreach (var profile in customerProfiles) 
            {
                foreach(var passport in profile.Passports) 
                {
                    var resultProfile = new ProfileSearchResponse(
                        profile.Id.ToString(),
                        $"{passport.LastName} {passport.OtherNames}",
                        passport.PassportNumber
                    );
                    resultsProfiles.Add(resultProfile);
                }
            }

            return resultsProfiles;
        }

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
