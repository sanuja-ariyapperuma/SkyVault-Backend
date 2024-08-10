//https://www.fileformat.info/tool/hash.htm

using SkyVault.Exceptions;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class SystemUserData(SkyvaultContext db)
    {
        public SkyResult<SystemUser> CreateOrGetUser(string[] requestUser, string? correlationId)
        {
            try
            {
                var sysUser = db.SystemUsers.FirstOrDefault(c => c.SamProfileId == requestUser[0]);

                if (sysUser != null)
                    return new SkyResult<SystemUser>().SucceededWithValue(sysUser);

                sysUser = new SystemUser
                {
                    FirstName = requestUser[2],
                    LastName = requestUser[1],
                    SamProfileId = requestUser[0],
                    UserRole = requestUser[3]
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
    }
}