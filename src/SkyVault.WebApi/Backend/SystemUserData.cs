//https://www.fileformat.info/tool/hash.htm

using Microsoft.EntityFrameworkCore;
using SkyVault.Exceptions;
using SkyVault.Payloads.CommonPayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class SystemUserData(SkyvaultContext db)
    {
        public SkyResult<SystemUser> CreateOrGetUser(SystemUserCreateOrUpdateDto requestUser, string? correlationId)
        {
            try
            {
                var sysUser = db.SystemUsers.FirstOrDefault(c => c.SamProfileId == requestUser.Upn);

                if (sysUser != null)
                    return new SkyResult<SystemUser>().SucceededWithValue(sysUser);

                sysUser = new SystemUser
                {
                    FirstName = requestUser.FirstName,
                    LastName = requestUser.LastName,
                    SamProfileId = requestUser.Upn,
                    UserRole = requestUser.UserRole.ToString()
                };
                
                db.SystemUsers.Add(sysUser);
                db.SaveChanges();
                
                return new SkyResult<SystemUser>().SucceededWithValue(sysUser);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);

                return new SkyResult<SystemUser>().Fail(
                    message: "An unexpected error occurred while creating user. Please try again.",
                    errorCode: "2ac5059f-0000",
                    correlationId: correlationId);
            }
        }

        public SkyResult<SystemUser> GetUserByProfileId(int sysUserId, string? correlationId)
        {
            try
            {
                var user = db.SystemUsers.Find(sysUserId);

                if (user == null)
                    return new SkyResult<SystemUser>().Fail(
                        message: "User not found.",
                        errorCode: "2ac5059f-0001",
                        correlationId: correlationId);

                return new SkyResult<SystemUser>().SucceededWithValue(user);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);
                
                return new SkyResult<SystemUser>().Fail(
                    message: "An unexpected error occurred while fetching user. Please try again.",
                    errorCode: "2ac5059f-0002",
                    correlationId: correlationId);
            }
        }

        public SkyResult<int> GetUserIdByUpn(string upn, string? correlationId) 
        {
            try
            {
                var user = db.SystemUsers.AsNoTracking().FirstOrDefault(c => c.SamProfileId == upn);

                if (user == null)
                    return new SkyResult<int>().Fail(
                        message: "User not found.",
                        errorCode: "2ac5059f-0003",
                        correlationId: correlationId);

                return new SkyResult<int>().SucceededWithValue(user.Id);
            }
            catch (Exception e)
            {
                e.LogException(correlationId);

                return new SkyResult<int>().Fail(
                    message: "An unexpected error occurred while fetching user id. Please try again.",
                    errorCode: "2ac5059f-0002",
                    correlationId: correlationId);
            }

        }

        public SkyResult<string> GetUserRoleByUpn(string upn, string? correlationId) 
        {
            try
            {
                var userRole = db.SystemUsers.AsNoTracking().FirstOrDefault(c => c.SamProfileId == upn)?.UserRole;

                if (String.IsNullOrEmpty(userRole))
                    return new SkyResult<string>().Fail(
                        message: "User role not found.",
                        errorCode: "2ac5059f-0004",
                        correlationId: correlationId);

                return new SkyResult<string>().SucceededWithValue(userRole);
            }
            catch (Exception e)
            {

                e.LogException(correlationId);

                return new SkyResult<string>().Fail(
                    message: "An unexpected error occurred while fetching user role. Please try again.",
                    errorCode: "2ac5059f-0002",
                    correlationId: correlationId);
            }
        }
    }
}