namespace SkyVault.WebApi.Endpoints;

internal static class LoginEndpointExtension
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/user", Workloads.AuthenticationWorkload.AuthenticateUser)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}