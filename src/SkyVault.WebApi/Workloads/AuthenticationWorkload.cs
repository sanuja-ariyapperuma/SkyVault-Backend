using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request, HttpContext context, SkyvaultContext dbContext)
    {
        var result = new SkyResult<WelcomeUserResponse>();

        if (string.IsNullOrWhiteSpace(request.Upn) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.Role)
           )
        {
            result.Fail("Invalid user information", "400", "0");

            return Results.BadRequest(result);
        }
        
        var SystemUserData = new SystemUserData(dbContext);
        var sysUser = SystemUserData.CreateOrGetUser(request);
        
        return Results.Ok(result.SucceededWithValue(new WelcomeUserResponse(sysUser.SamProfileId,
            $"{sysUser.FirstName} {sysUser.LastName}",
            "",
            sysUser.UserRole,
            sysUser.SamProfileId
        )));
    }
}