
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public static class SystemUserExtentions
    {
        public static async Task<WelcomeUserResponse> CreateOrGetUser(
            this DbSet<SystemUser> systemUser, 
            ValidateUserRequest requestUser, SkyvaultContext db)
        {
            var sysUser = await db.SystemUsers.FirstOrDefaultAsync(c => c.SamProfileId == requestUser.Upn);
            if (sysUser == null)
            {
                sysUser = new SystemUser
                {
                    FirstName = requestUser.FirstName,
                    LastName = requestUser.LastName,
                    SamProfileId = requestUser.Upn,
                    UserRole = requestUser.Role
                };

                await db.SystemUsers.AddAsync(sysUser);
                await db.SaveChangesAsync();
            }
            
            return new WelcomeUserResponse(sysUser.SamProfileId, 
                               $"{sysUser.FirstName} {sysUser.LastName}", 
                               "",
                               sysUser.UserRole,
                               sysUser.SamProfileId
                               );
        }

    }
}
