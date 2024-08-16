namespace SkyVault.WebApi.Endpoints
{
    public static class ProfileEndpointExtension
    {
        public static void MapProfileEndpoints(this WebApplication app)
        {

            //app.MapPost("/getprofile", Workloads.ProfileWorkload.GetProfile)
            //    .Produces(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/searchprofile", Workloads.ProfileWorkload.SearchProfiles)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addpassport", Workloads.ProfileWorkload.AddPassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            //app.MapPost("/getpassport", Workloads.ProfileWorkload.GetPassport)
            //    .Produces(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/UpdatePassport", Workloads.ProfileWorkload.UpdatePassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addvisa", Workloads.ProfileWorkload.AddVisa)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            //app.MapPost("/getvisa", Workloads.ProfileWorkload.GetVisa)
            //    .Produces(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updatevisa/{visaId}", Workloads.ProfileWorkload.UpdateVisa)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapDelete("/deleteVisa/{visaId}", Workloads.ProfileWorkload.DeleteVisa)
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status401Unauthorized);

            //app.MapPost("/checkpPassportNumber", Workloads.ProfileWorkload.CheckPassportExist)
            //    .Produces(StatusCodes.Status200OK)
            //    .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getVISAByCustomer", Workloads.ProfileWorkload.GetVisaByCustomerProfileId)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addFFN", Workloads.ProfileWorkload.AddFFN)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updateFFN/{ffId}", Workloads.ProfileWorkload.UpdateFFN)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapDelete("/deleteFFN/{ffId}", Workloads.ProfileWorkload.DeleteFFN)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getFFNByCustomer", Workloads.ProfileWorkload.GetFFNByCustomerId)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/updateCommMethod", Workloads.ProfileWorkload.UpdateComMethod)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getCommMethod", Workloads.ProfileWorkload.GetComMethod)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getPassportsByCustomerId", Workloads.ProfileWorkload.GetPassports)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
