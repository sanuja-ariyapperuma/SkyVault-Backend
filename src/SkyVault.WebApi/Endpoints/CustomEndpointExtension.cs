namespace SkyVault.WebApi.Endpoints;

internal static class CustomEndpointExtension
{
    public static void MapCustomEndpoints(this WebApplication app)
    {
        app.MapGet("/c-check", () => Task.FromResult("Health Check!"))
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/profilepage-commondata", Workloads.CustomWorkload.GetProfilePageDefinitionData).RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}