
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public static class SystemUserExtension
    {
        public static SystemUser CreateOrGetUser(
            this DbSet<SystemUser> systemUser, 
            ValidateUserRequest requestUser, SkyvaultContext db)
        {
            var sysUser = db.SystemUsers.FirstOrDefault(c => c.SamProfileId == requestUser.Upn);

            if (sysUser != null)
                return sysUser;
            
            sysUser = new SystemUser
            {
                FirstName = requestUser.FirstName,
                LastName = requestUser.LastName,
                SamProfileId = requestUser.Upn,
                UserRole = requestUser.Role
            };

            db.SystemUsers.Add(sysUser);
            db.SaveChanges();

            return sysUser;
        }
    }
}
