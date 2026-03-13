using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SkyVault.Exceptions;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads;

public static class CustomWorkload
{
    //Error Code vFxlOv
    public static SkyResult<ProfileDefinitionResponse> GetProfilePageDefinitionData(SkyvaultContext dbContext,
        IMapper mapper, IMemoryCache cache, IConfiguration configuration, HttpContext context, CacheService cacheService)
    {
        var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? "";

        List<Salutation> salutations = [];
        List<Nationality> nationalities = [];
        List<Country> countries = [];
        List<Gender> gender = [];

        try
        {
            salutations = cacheService.GetSalutations();
            countries = cacheService.GetCountries();
            nationalities = cacheService.GetNationalities();
            gender = cacheService.GetGender();

            var profileDefinition = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.CommonPayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.CommonPayloads.Nationality>>(nationalities),
                mapper.Map<List<Payloads.CommonPayloads.Gender>>(gender),
                mapper.Map<List<Payloads.CommonPayloads.Country>>(countries));

            return new SkyResult<ProfileDefinitionResponse>().SucceededWithValue(profileDefinition);
        }
        catch (Exception e)
        {
            e.LogException(correlationId);

            return new SkyResult<ProfileDefinitionResponse>().Fail(
                "An unexpected error occurred. Please try again later.",
                "vFxlOv-0001", correlationId);
        }
    }

    //Health check for api and database
    public static async Task<IResult> HealthCheckAsync(SkyvaultContext dbContext, IConfiguration configuration)
    {
        const string correlationId = "00000000-0000";
        try
        {
            var canConnect = await dbContext.Database.CanConnectAsync();
            if (canConnect)
            {
                return Results.Ok("API and Database are healthy");
            }
            else
            {
                return Results.Problem(new ProblemDetails().ToProblemDetails(
                    "Database cannot be connected",
                    "vFxlOv-0000",
                    correlationId));
            }
        }
        catch (Exception e)
        {
            e.LogException(correlationId);

            return Results.Problem(new ProblemDetails().ToProblemDetails(
                "An error occurred while checking the database connection",
                "vFxlOv-0000",
                correlationId));
        }
    }
}
