using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.Payloads.CommonPayloads;

namespace SkyVault.WebApi.Backend
{
    public sealed class CustomerProfileData(SkyvaultContext db)
    {
        public CustomerProfile? Get(int profileId, int systemUserId) =>
            db.CustomerProfiles.Include(p => p.Passports)
                .ThenInclude(o => o.Visas)
                .FirstOrDefault(p => p.Id == profileId);
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

                        p.OtherNames != null &&
                        p.OtherNames.Contains(searchQuery)

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
                    PreferredCommId = (int)PreferedCommiunicationMethod.None,
                    ParentId = string.IsNullOrWhiteSpace(passportRequest.ParentId) ? null : Convert.ToInt32(passportRequest.ParentId),
                    Passports = new List<Passport> { passport }
                };

                var savedprofile = db.CustomerProfiles.Add(newProfile);
                db.SaveChanges();

                return new SkyResult<CustomerProfile>().SucceededWithValue(savedprofile.Entity);

            }
            catch (Exception ex)
            {
                return new SkyResult<CustomerProfile>().Fail(ex.Message, "4cf0079e-0008", correlationId);
            }
        }

        public bool CheckPassportExists(string passportNumber, string correlationId)
        {
            try
            {
                return db.Passports.Any(p => p.PassportNumber == passportNumber);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public SkyResult<string> UpdateComMethod(ComMethodUpdateRequest comMethodRequest, string correlationId)
        {
            var isCommMethodExists = db.CommunicationMethods.Any(s => s.Id == Convert.ToInt32(comMethodRequest.PrefCommId));

            if (!isCommMethodExists)
                return new SkyResult<string>().Fail("Communication Method does not exist", "4cf0079e-0009", correlationId);

            var result = db.CustomerProfiles.Where(c =>
                c.Id == Convert.ToInt32(comMethodRequest.CustomerProfileId) &&
                c.SystemUserId == Convert.ToInt32(comMethodRequest.SystemUserId)
                ).ExecuteUpdate(update =>
                    update.SetProperty(cp => cp.PreferredCommId, Convert.ToInt32(comMethodRequest.PrefCommId))
            );

            if (result == 0)
                return new SkyResult<string>().Fail("No Profile Found or Unauthorize Update", "4cf0079e-0010", correlationId);

            return new SkyResult<string>().SucceededWithValue("Preffered Commiunication Method Updated");
        }

        public SkyResult<string> CheckAccessToTheProfile(int customerProfileId, int systemUserId, string correlationId)
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

            if (!result)
                return new SkyResult<string>().Fail("Unauthorized", "4cf0079e-0011", correlationId);

            return new SkyResult<string>().SucceededWithValue("Authorized");
        }

        public SkyResult<string> CheckAccessToTheProfileWithVisaId(int visaId, int systemUserId, string correlationId)
        {
            FormattableString query = $@"
                SELECT cp.Id 
                FROM customer_profiles cp
                INNER JOIN passports p 
                ON p.customer_profile_id = cp.id
                INNER JOIN visas v 
                ON v.passport_id = p.id
                WHERE v.id = {visaId}
                ";

            var customerProfileId = db.CustomerProfiles.FromSql(query).Select(p => p.Id).FirstOrDefault();

            return CheckAccessToTheProfile(customerProfileId, systemUserId, correlationId);
        }

        public SkyResult<PreferedCommiunicationMethod> GetComMethod(int customerProfileId, string correlationId)
        {
            var result = db.CustomerProfiles.Where(c =>
                c.Id == Convert.ToInt32(customerProfileId)
                ).Select(cp => cp.PreferredCommId).FirstOrDefault();

            if (result == 0)
                return new SkyResult<PreferedCommiunicationMethod>().Fail("No Profile Found or Unauthorize Update", "4cf0079e-0012", correlationId);

            return new SkyResult<PreferedCommiunicationMethod>().SucceededWithValue((PreferedCommiunicationMethod)result);
        }

        public SkyResult<string> CheckAccessToTheProfileWithPassportId(int passportId, int systemUserId, string correlationId)
        {
            FormattableString query = $@"
                SELECT cp.Id 
                FROM customer_profiles cp
                INNER JOIN passports p 
                ON p.customer_profile_id = cp.id
                WHERE p.id = {passportId}
                ";

            var customerProfileId = db.CustomerProfiles.FromSql(query).Select(p => p.Id).FirstOrDefault();

            return CheckAccessToTheProfile(customerProfileId, systemUserId, correlationId);
        }
    }
}
