using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Backend
{
    public sealed class CustomerProfileData(SkyvaultContext db)
    {
        public CustomerProfile? Get(int profileId, int systemUserId) =>
            db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefault(p => p.Id == profileId && p.SystemUserId == systemUserId);
        public List<SearchProfileItem>? Search(string searchQuery, int systemUserId)
        {
            string systemUserRoles = db.SystemUsers.Find(systemUserId)!.UserRole!;

            var query = db.Passports.Where(p => 
                (systemUserRoles == "admin" ||
                systemUserRoles == "su.admin" ||
                p.CustomerProfile.SystemUserId == systemUserId) &&
                (
                    p.PassportNumber.Contains(searchQuery) ||
                    p.LastName.Contains(searchQuery) ||
                    (
                        p.OtherNames != null && 
                        p.OtherNames.Contains(searchQuery)
                    )
                ));

            return query.Select(p => new SearchProfileItem
            (
                p.CustomerProfileId.ToString(),
                p.LastName,
                p.OtherNames,
                p.PassportNumber,
                p.CustomerProfile.Salutation.SalutationName
            )).ToList();
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
                    return new SkyResult<String>().Fail("Parent does not exist", "4cf0079e-0001", correlationId);
            }

            if (!String.IsNullOrWhiteSpace(passportRequest.CustomerProfileId)) 
            {
                var isCustomerProfileExists = db.CustomerProfiles.Any(s => s.Id == Convert.ToInt32(passportRequest.CustomerProfileId));
                if (!isCustomerProfileExists)
                    return new SkyResult<String>().Fail("Customer Profile does not exist", "4cf0079e-0002", correlationId);
            }

            if (!isSystemUserExists)
                return new SkyResult<String>().Fail("System User does not exist", "4cf0079e-0003", correlationId);

            if(!isSalutationExists)
                return new SkyResult<String>().Fail("Salutation does not exist", "4cf0079e-0004", correlationId);

            if (!isNationalityExists)
                return new SkyResult<String>().Fail("National does not exist", "4cf0079e-0005", correlationId);

            if (!isCountryExists)
                return new SkyResult<String>().Fail("Country does not exist", "4cf0079e-0006", correlationId);

            if (isPassportNumberExists)
                return new SkyResult<String>().Fail("Passport Number already exists", "4cf0079e-0007", correlationId);


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
                return new SkyResult<CustomerProfile>().Fail(ex.Message, "4cf0079e-0008", correlationId);
            }
        }
        public SkyResult<String> UpdateComMethod(ComMethodRequest comMethodRequest, string correlationId) 
        {
            var isCommMethodExists = db.CommunicationMethods.Any(s => s.Id == Convert.ToInt32(comMethodRequest.PrefCommId));

            if (!isCommMethodExists)
                return new SkyResult<String>().Fail("Communication Method does not exist", "4cf0079e-0009", correlationId);

            var result = db.CustomerProfiles.Where(c => 
                c.Id == Convert.ToInt32(comMethodRequest.CustomerProfileId) &&
                c.SystemUserId == Convert.ToInt32(comMethodRequest.SystemUserId)
                ).ExecuteUpdate(update => 
                    update.SetProperty(cp => cp.PreferredCommId , Convert.ToInt32(comMethodRequest.PrefCommId))
            );

            if (result == 0)
                return new SkyResult<String>().Fail("No Profile Found or Unauthorize Update", "4cf0079e-0010", correlationId);

            return new SkyResult<String>().SucceededWithValue("Preffered Commiunication Method Updated");
        }

        public SkyResult<String> CheckAccessToTheProfile(int customerProfileId, int systemUserId, string correlationId)
        {
            var systemUserRole = db.SystemUsers.Find(systemUserId)?.UserRole;

            var result = db.CustomerProfiles.Any(p => 
                p.Id == customerProfileId && 
                (
                    systemUserRole == "admin" ||
                    systemUserRole == "su.admin" ||
                    p.SystemUserId == systemUserId
                )
            );

            if(!result)
                return new SkyResult<string>().Fail("Unauthorized", "4cf0079e-0011", correlationId);

            return new SkyResult<string>().SucceededWithValue("Authorized");
        }
    }
}
