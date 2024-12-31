using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.Payloads.CommonPayloads;
using System.Globalization;

namespace SkyVault.WebApi.Backend
{
    public sealed class CustomerProfileData(SkyvaultContext db)
    {
        public List<SearchProfileItem>? Search(string searchQuery, string systemUserUniqueIdentifier)
        {
            var systemUser = db.SystemUsers.AsNoTracking().FirstOrDefault(e => e.SamProfileId == systemUserUniqueIdentifier);
            var systemUserRole = systemUser!.UserRole;
            var searchQueryUpper = searchQuery.ToUpper();

            var query = db.Passports.Where(p =>
                (
                    systemUserRole!.ToLower() == "superadmin" ||
                    systemUserRole!.ToLower() == "admin" ||
                    p.CustomerProfile.SystemUserId == systemUser.Id
                ) &&
                (
                    p.PassportNumber.ToUpper().Contains(searchQueryUpper) ||
                    p.LastName.ToUpper().Contains(searchQueryUpper) ||

                        p.OtherNames != null &&
                        p.OtherNames.ToUpper().Contains(searchQueryUpper)

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
        
        public SkyResult<CustomerProfile> SaveProfile(PassportRequest passportRequest, int systemUserId, string correlationId)
        {
                var passport = new Passport
                {
                    PassportNumber = passportRequest.PassportNumber!,
                    LastName = passportRequest.LastName!,
                    OtherNames = passportRequest.OtherNames,
                    CountryId = Convert.ToInt32(passportRequest.CountryId),
                    DateOfBirth = DateOnly.FromDateTime(DateTime.ParseExact(passportRequest.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    ExpiryDate = DateOnly.FromDateTime(DateTime.ParseExact(passportRequest.ExpiryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    Gender = passportRequest.Gender!,
                    IsPrimary = passportRequest.IsPrimary!,
                    NationalityId = Convert.ToInt32(passportRequest.NationalityId)
                };

                var newProfile = new CustomerProfile
                {
                    SystemUserId = Convert.ToInt32(systemUserId),
                    SalutationId = Convert.ToInt32(passportRequest.SalutationId),
                    PreferredCommId = (int)PreferedCommiunicationMethod.None,
                    ParentId = string.IsNullOrWhiteSpace(passportRequest.ParentId) ? null : Convert.ToInt32(passportRequest.ParentId),
                    Passports = new List<Passport> { passport }
                };

                var savedprofile = db.CustomerProfiles.Add(newProfile);
                db.SaveChanges();

                return new SkyResult<CustomerProfile>().SucceededWithValue(savedprofile.Entity);

            
        }
        public bool CheckPassportExists(string passportNumber)
            => db.Passports.Any(p => p.PassportNumber == passportNumber);

        public SkyResult<string> UpdateComMethod(ComMethodUpdateRequest comMethodRequest, string correlationId)
        {
            var isCommMethodExists = db.CommunicationMethods.Any(s => s.Id == Convert.ToInt32(comMethodRequest.PrefCommId));

            if (!isCommMethodExists)
                return new SkyResult<string>().Fail("Communication Method does not exist", "4cf0079e-0009", correlationId);

            var result = db.CustomerProfiles.Where(c =>
                c.Id == Convert.ToInt32(comMethodRequest.CustomerProfileId)
                ).ExecuteUpdate(update =>
                    update
                    .SetProperty(cp => cp.PreferredCommId, Convert.ToInt32(comMethodRequest.PrefCommId))
                    .SetProperty(cp => cp.WhatsAppNumber, comMethodRequest.WhatsAppNumber)
                    .SetProperty(cp => cp.EmailAddress, comMethodRequest.EmailAddress)
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
                return new SkyResult<string>().Fail("Sorry! You are not authorized to access this profile", "4cf0079e-0011", correlationId);

            return new SkyResult<string>().SucceededWithValue("Authorized");
        }

        public SkyResult<ComMethod> GetComMethod(int customerProfileId, string correlationId)
        {
            var result = db.CustomerProfiles.Where(c =>
                c.Id == Convert.ToInt32(customerProfileId)
                ).AsNoTracking().FirstOrDefault();

            if (result == null)
                return new SkyResult<ComMethod>().Fail("No Profile Found or Unauthorize Access", "4cf0079e-0012", correlationId);

            var response = new ComMethod()
            {
                CommunicationMethod = result.PreferredCommId,
                Email = result.EmailAddress,
                WhatsAppNumber = result.WhatsAppNumber

            };

            return new SkyResult<ComMethod>().SucceededWithValue(response);
        }
        
        public SkyResult<List<FamilyMembersResponse>> GetFamilyMembers(int customerId, string correlationId) 
        {
            
            var isCustomerParent = db.CustomerProfiles.AsNoTracking().Where(cp => cp.Id == customerId && cp.ParentId == null).Any();
            var familyMembers = new List<FamilyMembersResponse>();

            if (isCustomerParent) // Parent
            {
                var customerQuery = db.CustomerProfiles
                    .Include(cp => cp.Passports)
                    .Include(cp => cp.InverseParent) // children profiles
                        .ThenInclude(child => child.Passports) // include children's passports
                    .Where(cp => cp.Id == customerId);

                var parent = customerQuery.FirstOrDefault();

                //Add Parent
                familyMembers.Add(new FamilyMembersResponse
                (
                    parent!.Id,
                    parent.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.LastName,
                    parent.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.OtherNames ?? "",
                    parent.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.PassportNumber,
                    true
                ));

                //Add Children
                familyMembers.AddRange(parent.InverseParent.Select(child => new FamilyMembersResponse
                (
                    child.Id,
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.LastName,
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.OtherNames ?? "",
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.PassportNumber,
                    false
                )).OrderBy(a => a.OtherNames));
            }
            else // Child
            {
                var customerQuery = db.CustomerProfiles
                    .Include(cp => cp.Parent)
                        .ThenInclude(p => p.Passports)
                    .Include(p => p.Parent)
                        .ThenInclude(parent => parent.InverseParent)
                        .ThenInclude(child => child.Passports)
                    .Where(cp => cp.Id == customerId);

                var result = customerQuery.ToList();

                //Add Parent
                familyMembers.Add(new FamilyMembersResponse
                (
                    result[0].Parent!.Id,
                    result[0].Parent!.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.LastName,
                    result[0].Parent!.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.OtherNames ?? "",
                    result[0].Parent!.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.PassportNumber,
                    true
                ));

                //Add Children
                familyMembers.AddRange(result[0].Parent!.InverseParent.Select(child => new FamilyMembersResponse
                (
                    child.Id,
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.LastName,
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.OtherNames ?? "",
                    child.Passports.FirstOrDefault(p => p.IsPrimary == "1")!.PassportNumber,
                    false
                )).OrderBy(a => a.OtherNames));
            }

            return new SkyResult<List<FamilyMembersResponse>>().SucceededWithValue(familyMembers);

        }

        public SkyResult<int> GetCustomerProfileIdByVisaId(int visaId, string correlationId) 
        {
            var customerProfileId = db.Visas
                .Include(u => u.Passport).FirstOrDefault(a => a.Id == visaId)?
                .Passport.CustomerProfileId;

            if (customerProfileId == null)
                return new SkyResult<int>().Fail("No profile found", "", correlationId);

            return new SkyResult<int>().SucceededWithValue((int)customerProfileId);
        }
    }
}
