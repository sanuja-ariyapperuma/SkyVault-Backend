using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkyVault.Exceptions;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads;

public static class CustomWorkload
{
    public static IResult GetProfilePageDefinitionData(SkyvaultContext dbContext,
        IMapper mapper, IConfiguration configuration, HttpContext context)
    {
        var correlationId = context.Items["X-Correlation-ID"]?.ToString();

        List<Salutation> salutations = [];
        List<Nationality> nationalities = [];
        List<Country> countries = [];

        try
        {
            var tasks = new Task[]
            {
                    Task.Run(() =>
                    {
                        var commonData = new CommonData(dbContext.CreateDbContext());
                        salutations = commonData.Salutations();
                    }),
                    Task.Run(() =>
                    {
                        var commonData = new CommonData(dbContext.CreateDbContext());
                        nationalities = commonData.GetNationalities();
                    }),
                    Task.Run(() =>
                    {
                        var commonData = new CommonData(dbContext.CreateDbContext());
                        countries = commonData.GetCountries();
                    })
            };

            Task.WaitAll(tasks);

            var commonData = new CommonData(dbContext);
            var genders = commonData.GetGender();

            var profileDefinition = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.CommonPayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.CommonPayloads.Nationality>>(nationalities),
                mapper.Map<List<Payloads.CommonPayloads.Gender>>(genders),
                mapper.Map<List<Payloads.CommonPayloads.Country>>(countries));

            return Results.Ok(profileDefinition);
        }
        catch (Exception e)
        {
            e.LogException(correlationId);

            return Results.Problem(new ProblemDetails().ToProblemDetails(
                "An unexpected error occurred. Please try again later.",
                "2adb05bf-0000", correlationId));
        }
    }
}
