using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Workloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApi.Endpoints;

internal static class LoginEndpointExtension
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/user", ([FromBody] LoginUserRequest request, HttpContext context, SkyvaultContext dbContext) =>
        {
            var result = Workloads.AuthenticationWorkload.AuthenticateUser(request, context, dbContext);
            
            if (!result.Succeeded)
                return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message, result.ErrorCode, result.CorrelationId));
            
            return Results.Ok();
        })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}