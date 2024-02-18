namespace SkyVault.WebApi.Endpoints;

internal static class CustomerEndpointExtension
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGet("/c-check", () => Task.FromResult("Health Check!"))
            .Produces(StatusCodes.Status200OK);
    }
}