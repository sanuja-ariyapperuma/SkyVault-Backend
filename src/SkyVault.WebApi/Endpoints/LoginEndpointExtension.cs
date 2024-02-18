namespace SkyVault.WebApi.Endpoints;

internal static class LoginEndpointExtension
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapGet("/l-check", () => Task.FromResult("Health Check!"))
            .Produces(StatusCodes.Status200OK);
        
        app.MapPost("/auth/user", Workloads.AuthenticationWorkload.AuthenticateUser)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}