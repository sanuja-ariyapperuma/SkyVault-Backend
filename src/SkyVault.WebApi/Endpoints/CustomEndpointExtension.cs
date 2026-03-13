using SkyVault.Payloads.ResponsePayloads;
using SkyVault.Payloads.RequestPayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Workloads;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

namespace SkyVault.WebApi.Endpoints;

internal static class CustomEndpointExtension
{
    public static void MapCustomEndpoints(this WebApplication app)
    {
        app.MapGet("/api/health", Workloads.CustomWorkload.HealthCheckAsync)
            .Produces(StatusCodes.Status200OK);

        app.MapPost("/customerProfileCommonData", (SkyvaultContext dbContext, IMapper mapper, IMemoryCache cache, IConfiguration configuration, HttpContext context, CacheService cacheService) =>
        {
            var result = Workloads.CustomWorkload.GetProfilePageDefinitionData(dbContext, mapper, cache, configuration, context, cacheService);
            
            if (!result.Succeeded)
                return Results.Problem(new ProblemDetails().ToProblemDetails(result.Message, result.ErrorCode, result.CorrelationId));
            
            return Results.Ok(result.Value);
        })
            .RequireAuthorization()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}