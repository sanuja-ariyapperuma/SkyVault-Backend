namespace SkyVault.WebApi.Endpoints
{
    public static class ProfileEndpointExtention
    {
        public static void MapProfileEndpoints(this WebApplication app)
        {
            app.MapPost("/profile", Workloads.ProfileWorkload.SaveProfile)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getprofile", Workloads.ProfileWorkload.GetProfile)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/searchprofile", Workloads.ProfileWorkload.SearchProfiles)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
