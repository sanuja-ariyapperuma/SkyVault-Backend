using Microsoft.AspNetCore.Mvc;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;

namespace SkyVault.WebApi.Workloads;

internal static class AuthenticationWorkload
{
    public static IResult AuthenticateUser([FromBody] ValidateUserRequest request, HttpContext context)
    {
        //If the user does not exist, user needs to be created or else information about the user needs to be returned.
        //Access to the portal is granted through the Azure portal for this sites app registration.
        
        return Results.Ok(new WelcomeUserResponse("ayub@hsenidbiz.com", "Ayub Sourjah", 
            "Today", "Super-Admin", "ayub@hsenidbiz.com"));
    }
}