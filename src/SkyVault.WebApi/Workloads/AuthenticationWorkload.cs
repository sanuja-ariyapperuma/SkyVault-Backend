using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request, HttpContext context)
    {
        return Results.Ok(new WelcomeUserResponse("Test", "Today @ 9.30am"));
    }
}