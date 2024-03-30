using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static async Task<SkyResult<WelcomeUserResponse>> AuthenticateUser([FromBody] ValidateUserRequest request, HttpContext context, SkyvaultContext dbContext)
    {
        var response = new SkyResult<WelcomeUserResponse>();

        if (string.IsNullOrWhiteSpace(request.Upn) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.Role)
            )
        {
            response.Fail("Invalid user information", "400", "0");
        }
        else 
        {
            var systemUser = await dbContext.SystemUsers.CreateOrGetUser(request, dbContext);
            response.SucceededWithValue(systemUser);
        }

        return response;

        //Access to the portal is granted through the Azure portal for this sites app registration.

    }
}