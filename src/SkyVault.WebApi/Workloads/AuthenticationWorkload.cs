using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static async Task<IResult> AuthenticateUser([FromBody] ValidateUserRequest request, HttpContext context, SkyvaultContext dbContext)
    {
        if (string.IsNullOrWhiteSpace(request.Upn) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Role)
            )
            return Results.BadRequest("Invalid user information.");

        var systemUser = await dbContext.SystemUsers.CreateOrGetUser(request, dbContext);
        return Results.Ok(systemUser);

        //Access to the portal is granted through the Azure portal for this sites app registration.

    }
}