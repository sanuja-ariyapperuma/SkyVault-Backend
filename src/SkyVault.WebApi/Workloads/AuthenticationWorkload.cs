using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;
using System.Security.Claims;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request,
        HttpContext context, SkyvaultContext dbContext)
    {
        var correlationId = context.Items["X-Correlation-ID"]?.ToString();

        var firstname = context.User.FindFirst("name")?.Value;
        var lastName = context.User.FindFirst(ClaimTypes.Surname)?.Value;
        var email = context.User.FindFirst(ClaimTypes.GivenName)?.Value;
        var role = context.User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        var roleClaim = context.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        var role2 = context.User.Claims.Where(x => x.Type == "role")?.Select(c => c.Value);

        //surname, givenname

        var loginUser = new[]
        {
            request.Upn,
            lastName,
            firstname,
            "Staff"
        };

        var systemUserData = new SystemUserData(dbContext);
        var result = systemUserData.CreateOrGetUser(loginUser, correlationId);

        if (!result.Succeeded)
            return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message,
                result.ErrorCode, result.CorrelationId));

        var sysUser = result.Value;

        return Results.Ok(new WelcomeUserResponse(
            sysUser!.Id.ToString(),
            sysUser!.SamProfileId,
            $"{sysUser.FirstName} {sysUser.LastName}",
            DateTime.Today.ToLongDateString(),
            sysUser.UserRole,
            sysUser.SamProfileId
        ));
    }
}