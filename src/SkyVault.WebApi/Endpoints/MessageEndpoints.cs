namespace SkyVault.WebApi.Endpoints
{
    internal static class MessageEndpoints
    {
        public static void MapMessageEndpoints(this WebApplication app)
        {
            app.MapPost("/GetPreSignedUrl", Workloads.MessageWorkload.GetPreSignedUrl)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);


            app.MapPost("/UpdateBirthdayFile", Workloads.MessageWorkload.UpdateBirthdayFile)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/GetMessage", Workloads.MessageWorkload.GetMessage)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);


        }
    }
}
