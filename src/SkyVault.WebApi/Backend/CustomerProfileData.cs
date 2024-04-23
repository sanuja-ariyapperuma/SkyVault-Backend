using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CustomerProfileData(SkyvaultContext db)
    {
        public CustomerProfile? Get(int profileId, int systemUserId) =>
            db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefault(p => p.Id == profileId && p.SystemUserId == systemUserId);

        public List<CustomerProfile>? Search(string searchQuery, int systemUserId)
        {
            string systemUserRoles = db.SystemUsers.Find(systemUserId)!.UserRole!;

            var query = db.CustomerProfiles
                .Include(p => p.Passports)
                .Include(p => p.Salutation)
                .Where(c => (systemUserRoles == "admin" ||
                             systemUserRoles == "su.admin" ||
                             c.SystemUserId == systemUserId) &&
                            (c.Passports.Any(p => p.PassportNumber.Contains(searchQuery)) ||
                             c.Passports.Any(p => p.LastName.Contains(searchQuery)) ||
                             c.Passports.Any(p => p.OtherNames!.Contains(searchQuery))));

            return query.Select(p => new CustomerProfile
            {
                Id = p.Id,
                Salutation = p.Salutation,
                Passports = p.Passports
            }).ToList();
        }


        public CustomerProfile Create(CustomerProfile newProfile)
        {
            var savedprofile = db.CustomerProfiles.Add(newProfile);
            db.SaveChanges();

            return savedprofile.Entity;
        }

        public SkyResult<String> ValidateProfile(PassportRequest passportRequest, string correlationId) 
        {
            var isSystemUserExists = db.SystemUsers.Any(s => s.Id == Convert.ToInt32(passportRequest.SystemUserId));
            var isSalutationExists = db.Salutations.Any(s => s.Id == Convert.ToInt32(passportRequest.SalutationId));
            var isNationalityExists = db.Nationalities.Any(s => s.Id == Convert.ToInt32(passportRequest.NationalityId));
            var isCountryExists = db.Countries.Any(s => s.Id == Convert.ToInt32(passportRequest.CountryId));
            var isPassportNumberExists = db.Passports.Any(s => s.PassportNumber == passportRequest.PassportNumber);

            if (!String.IsNullOrWhiteSpace(passportRequest.ParentId)) 
            {
                var isParentExists = db.CustomerProfiles.Any(s => s.Id == Convert.ToInt32(passportRequest.ParentId));
                if (!isParentExists)
                    return new SkyResult<String>().Fail("Parent does not exist", "4cf0079e-0004", correlationId);
            }

            if (!String.IsNullOrWhiteSpace(passportRequest.CustomerProfileId)) 
            {
                var isCustomerProfileExists = db.CustomerProfiles.Any(s => s.Id == Convert.ToInt32(passportRequest.CustomerProfileId));
                if (!isCustomerProfileExists)
                    return new SkyResult<String>().Fail("Customer Profile does not exist", "4cf0079e-0004", correlationId);
            }

            if (!isSystemUserExists)
                return new SkyResult<String>().Fail("System User does not exist", "4cf0079e-0000", correlationId);

            if(!isSalutationExists)
                return new SkyResult<String>().Fail("Salutation does not exist", "4cf0079e-0001", correlationId);

            if (!isNationalityExists)
                return new SkyResult<String>().Fail("National does not exist", "4cf0079e-0002", correlationId);

            if (!isCountryExists)
                return new SkyResult<String>().Fail("Country does not exist", "4cf0079e-0003", correlationId);

            if (isPassportNumberExists)
                return new SkyResult<String>().Fail("Passport Number already exists", "4cf0079e-0006", correlationId);


            return new SkyResult<String>().SucceededWithValue("Validated");
        }

        public SkyResult<CustomerProfile> SaveProfile(PassportRequest passportRequest, string correlationId)
        {
            try
            {
                var passport = new Passport
                {
                    PassportNumber = passportRequest.PassportNumber!,
                    LastName = passportRequest.LastName!,
                    OtherNames = passportRequest.OtherNames,
                    CountryId = Convert.ToInt32(passportRequest.CountryId),
                    DateOfBirth = DateOnly.FromDateTime(DateTime.Parse(passportRequest.DateOfBirth)),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.Parse(passportRequest.ExpiryDate)),
                    Gender = passportRequest.Gender!,
                    IsPrimary = passportRequest.IsPrimary!,
                    NationalityId = Convert.ToInt32(passportRequest.NationalityId),
                    PlaceOfBirth = passportRequest.PlaceOfBirth
                };

                var newProfile = new CustomerProfile
                {
                    SystemUserId = Convert.ToInt32(passportRequest.SystemUserId),
                    SalutationId = Convert.ToInt32(passportRequest.SalutationId),
                    PreferredCommId = 1,
                    ParentId = String.IsNullOrWhiteSpace(passportRequest.ParentId) ? null : Convert.ToInt32(passportRequest.ParentId),
                    Passports = new List<Passport> { passport } 
                };

                var savedprofile = db.CustomerProfiles.Add(newProfile);
                db.SaveChanges();

                return new SkyResult<CustomerProfile>().SucceededWithValue(savedprofile.Entity);

            }
            catch(Exception ex)
            {
                return new SkyResult<CustomerProfile>().Fail(ex.Message, "4cf0079e-0005", correlationId);
            }
        }

        public SkyResult<String> UpdateComMethod(ComMethodRequest comMethodRequest, string correlationId) 
        {
            var isCommMethodExists = db.CommunicationMethods.Any(s => s.Id == Convert.ToInt32(comMethodRequest.PrefCommId));

            if (!isCommMethodExists)
                return new SkyResult<String>().Fail("Communication Method does not exist", "4cf0079e-0007", correlationId);

            var result = db.CustomerProfiles.Where(c => 
                c.Id == Convert.ToInt32(comMethodRequest.CustomerProfileId) &&
                c.SystemUserId == Convert.ToInt32(comMethodRequest.SystemUserId)
                ).ExecuteUpdate(update => 
                    update.SetProperty(cp => cp.PreferredCommId , Convert.ToInt32(comMethodRequest.PrefCommId))
            );

            if (result == 0)
                return new SkyResult<String>().Fail("No Profile Found or Unauthorize Update", "4cf0079e-0008", correlationId);

            return new SkyResult<String>().SucceededWithValue("Preffered Commiunication Method Updated");
        }
    }
}
