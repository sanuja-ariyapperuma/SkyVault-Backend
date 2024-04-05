using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class SystemUserData(SkyvaultContext _db)
    {
        public SystemUser CreateOrGetUser(ValidateUserRequest requestUser)
        {
            var sysUser = _db.SystemUsers.FirstOrDefault(c => c.SamProfileId == requestUser.Upn);

            if (sysUser != null)
                return sysUser;

            sysUser = new SystemUser
            {
                FirstName = requestUser.FirstName,
                LastName = requestUser.LastName,
                SamProfileId = requestUser.Upn,
                UserRole = requestUser.Role
            };

            _db.SystemUsers.Add(sysUser);
            _db.SaveChanges();

            return sysUser;
        }

        public SystemUser? GetUserByProfileId(int sysUserId)
        {
            return _db.SystemUsers.Find(sysUserId);
        }
    }
}
