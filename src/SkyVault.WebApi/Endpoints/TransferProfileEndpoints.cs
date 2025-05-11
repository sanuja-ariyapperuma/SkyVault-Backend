namespace SkyVault.WebApi.Endpoints
{
    public static class TransferProfileEndpoints
    {
        public static void MapTransferProfileEndpoints(this WebApplication app) 
        {
            app.MapGet("/Staff", Workloads.TransferProfileWorkload.GetAllStaff)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/GetCustomers", Workloads.TransferProfileWorkload.GetCustomersForStaffId)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/TransferProfiles", Workloads.TransferProfileWorkload.TransferProfiles)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
        
    }
}
