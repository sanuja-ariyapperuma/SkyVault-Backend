namespace SkyVault.WebApi.Endpoints;

internal static class LoginEndpointExtension
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapGet("/l-check", () => Task.FromResult("Health Check!"));
        app.MapPost("/auth/user", Workloads.AuthenticationWorkload.AuthenticateUser)
            .RequireAuthorization();
    }
}