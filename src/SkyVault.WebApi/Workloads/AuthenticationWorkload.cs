using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request, 
        HttpContext context, SkyvaultContext dbContext)
    {
        var propertiesToCheck = new[] {request.Upn, request.Email, 
            request.LastName, request.FirstName, request.Role};

        if (propertiesToCheck.Any(string.IsNullOrWhiteSpace))
        {
            return Results.BadRequest(request);
        }
        
        var sysUser =  dbContext.SystemUsers.CreateOrGetUser(request, dbContext);
        
        return Results.Ok(new WelcomeUserResponse(sysUser.SamProfileId, 
            $"{sysUser.FirstName} {sysUser.LastName}", 
            DateTime.Today.ToLongDateString(),
            sysUser.UserRole,
            sysUser.SamProfileId
        ));
    }
}