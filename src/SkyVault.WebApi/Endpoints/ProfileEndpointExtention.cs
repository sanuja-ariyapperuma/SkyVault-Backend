namespace SkyVault.WebApi.Endpoints
{
    public static class ProfileEndpointExtension
    {
        public static void MapProfileEndpoints(this WebApplication app)
        {

            app.MapPost("/getprofile", Workloads.ProfileWorkload.GetProfile)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/searchprofile", Workloads.ProfileWorkload.SearchProfiles)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addpassport", Workloads.ProfileWorkload.AddPassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getpassport", Workloads.ProfileWorkload.GetPassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updatepassport", Workloads.ProfileWorkload.UpdatePassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addvisa", Workloads.ProfileWorkload.AddVisa)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getvisa", Workloads.ProfileWorkload.GetVisa)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updatevisa", Workloads.ProfileWorkload.UpdateVisa)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updateCommMethod", Workloads.ProfileWorkload.UpdateComMethod)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/checkpPassportNumber", Workloads.ProfileWorkload.CheckPassportExist)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
