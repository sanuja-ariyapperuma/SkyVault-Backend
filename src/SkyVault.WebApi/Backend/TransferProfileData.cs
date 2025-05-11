using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class TransferProfileData
    {
        private readonly SkyvaultContext _db;
        public TransferProfileData(SkyvaultContext db)
        {
            _db = db;
        }

        public async Task<SkyResult<List<SystemUser>>> GetAllStaff()
        {
            try
            {
                var staff = await _db.SystemUsers
                .AsNoTracking()
                .Where(u => u.Active == "1")
                .ToListAsync();

                return new SkyResult<List<SystemUser>>().SucceededWithValue(staff);
            }
            catch (Exception ex)
            {
                ex.LogException("");
                return new SkyResult<List<SystemUser>>().Fail(ex.Message, "500", null);
            }
        }

        public async Task<SkyResult<List<CustomerProfile>>> GetCustomersForStaffId(int StaffId)
        {
            try
            {
                var customers = await _db.CustomerProfiles
                .Include(customer => customer.Passports)
                .Include(customer => customer.Salutation)
                .AsNoTracking()
                .Where(u => u.SystemUserId == StaffId && u.ParentId == null)  // Removing child profiles to overcome the conflict of transfering child profile into another staff member
                .ToListAsync();

                return new SkyResult<List<CustomerProfile>>().SucceededWithValue(customers);
            }
            catch (Exception ex)
            {
                ex.LogException("");
                return new SkyResult<List<CustomerProfile>>().Fail(ex.Message, "500", null);
            }
        }

        public async Task<SkyResult<string>> UpdateProfileTransfer(int staffid, int[] profileIds)
        {
            try
            {
                var childProfileIds = await _db.CustomerProfiles
                    .Where(p => profileIds.Contains(p.ParentId ?? 0))
                    .Select(p => p.Id)
                    .ToListAsync();

                var allProfileIds = profileIds.Concat(childProfileIds).Distinct().ToArray();

                await _db.CustomerProfiles
                    .Where(p => allProfileIds.Contains(p.Id))
                    .ExecuteUpdateAsync(setters => setters.SetProperty(p => p.SystemUserId, staffid));

                return new SkyResult<string>().SucceededWithValue(profileIds.Length.ToString());
            }
            catch (Exception ex)
            {
                ex.LogException("");
                return new SkyResult<string>().Fail(ex.Message, "500", null);
            }
        }
    }
}
