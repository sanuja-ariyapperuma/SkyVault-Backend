using SkyVault.Payloads.RequestPayloads;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Workloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;
using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApi.Endpoints
{
    public static class TransferProfileEndpoints
    {
        public static void MapTransferProfileEndpoints(this WebApplication app) 
        {
            app.MapGet("/Staff", async (SkyvaultContext dbContext) =>
            {
                var result = await Workloads.TransferProfileWorkload.GetAllStaff(dbContext);
                
                if (!result.Succeeded)
                    return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message, result.ErrorCode, result.CorrelationId));
                
                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/GetCustomers", async ([FromBody] GetClientsForStaffIdRequest request, SkyvaultContext dbContext) =>
            {
                var result = await Workloads.TransferProfileWorkload.GetCustomersForStaffId(request, dbContext);
                
                if (!result.Succeeded)
                    return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message, result.ErrorCode, result.CorrelationId));
                
                return Results.Ok(result.Value);
            })
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            app.MapPost("/TransferProfiles", async ([FromBody] TransferProfileRequest request, SkyvaultContext dbContext) =>
            {
                var result = await Workloads.TransferProfileWorkload.TransferProfiles(request, dbContext);
                
                if (!result.Succeeded)
                    return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message, result.ErrorCode, result.CorrelationId));
                
                return Results.Ok();
            })
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);
        }
        
    }
}
