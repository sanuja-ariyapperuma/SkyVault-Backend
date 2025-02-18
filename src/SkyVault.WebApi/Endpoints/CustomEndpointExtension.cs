namespace SkyVault.WebApi.Endpoints;

internal static class CustomEndpointExtension
{
    public static void MapCustomEndpoints(this WebApplication app)
    {
        app.MapGet("/api-check", Workloads.CustomWorkload.HealthCheckAsync)
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/customerProfileCommonData", Workloads.CustomWorkload.GetProfilePageDefinitionData)
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}