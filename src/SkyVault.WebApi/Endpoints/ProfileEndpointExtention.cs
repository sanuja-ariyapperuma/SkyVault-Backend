namespace SkyVault.WebApi.Endpoints
{
    public static class ProfileEndpointExtension
    {
        public static void MapProfileEndpoints(this WebApplication app)
        {

            app.MapPost("/searchprofile", Workloads.ProfileWorkload.SearchProfiles)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addpassport", Workloads.ProfileWorkload.AddPassport)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/UpdatePassport", Workloads.ProfileWorkload.UpdatePassport)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addvisa", Workloads.ProfileWorkload.AddVisa)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPatch("/updatevisa/{visaId}", Workloads.ProfileWorkload.UpdateVisa)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapDelete("/deleteVisa/{visaId}", Workloads.ProfileWorkload.DeleteVisa)
                .RequireAuthorization()
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/getVISAByCustomerProfileId/{profileId}", Workloads.ProfileWorkload.GetVisaByCustomerProfileId)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/addFFN", Workloads.ProfileWorkload.AddFFN)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPut("/updateFFN/{ffId}", Workloads.ProfileWorkload.UpdateFFN)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapDelete("/deleteFFN/{ffId}", Workloads.ProfileWorkload.DeleteFFN)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/getFFNByCustomer/{profileId}", Workloads.ProfileWorkload.GetFFNByCustomerId)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/updateCommMethod", Workloads.ProfileWorkload.UpdateComMethod)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getCommMethod", Workloads.ProfileWorkload.GetComMethod)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/getPassportsByCustomerProfileId/{profileId}", Workloads.ProfileWorkload.GetPassports)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/getFamilyMembers/{profileId}", Workloads.ProfileWorkload.GetFamilyMembers)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
