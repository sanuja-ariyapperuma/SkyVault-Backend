namespace SkyVault.WebApi.Endpoints
{
    public static class ProfileEndpointExtention
    {
        public static void MapProfileEndpoints(this WebApplication app)
        {
            app.MapPost("/profile", Workloads.ProfileWorkload.SaveProfile)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/profile/{Id}/{sysUserId}", Workloads.ProfileWorkload.GetProfile)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/profile/{SearchQuery}/{SysUserId}/{RoleId}", Workloads.ProfileWorkload.GetAllProfiles)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
