using SkyVault.WebApi.Filters;

namespace SkyVault.WebApi.Endpoints
{
    internal static class MessageEndpoints
    {
        /**
         * When defining endpoints order is important. 
         * First check the authentication and then authorization always and so on for other filters.
         */
        public static void MapMessageEndpoints(this WebApplication app)
        {
            app.MapPost("/GetPreSignedUrl", Workloads.MessageWorkload.GetPreSignedUrl)
                .RequireAuthorization()
                .AddEndpointFilter<AdminOnlyFilter>()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);


            app.MapPost("/UpdateAttachmentFile", Workloads.MessageWorkload.UpdateAttachmentFile)
                .RequireAuthorization()
                .AddEndpointFilter<AdminOnlyFilter>()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapGet("/GetMessage", Workloads.MessageWorkload.GetMessage)
                .RequireAuthorization()
                .AddEndpointFilter<AdminOnlyFilter>()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/SaveAndSendPromotion", Workloads.MessageWorkload.SaveAndSendPromotion)
                .RequireAuthorization()
                .AddEndpointFilter<AdminOnlyFilter>()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);


        }
    }
}
