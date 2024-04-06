using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;
using SkyVault.WebApi.Helper;

namespace SkyVault.WebApi.Workloads
{
    internal static class CustomWorkload
    {
        public static IResult GetProfilePageDefinitionData(SkyvaultContext dbContext, IMapper mapper,
                       IConfiguration configuration)
        {
            //var commonData = new CommonData(CreateDbContext());

            //var salutations = commonData.Salutations();
            //var nationalities = commonData.GetNationalities();
            //var countries = commonData.GetCountries();
            List<Backend.Models.Salutation> salutations = new();
            List<Backend.Models.Nationality> nationalities = new();
            List<Backend.Models.Country> countries = new();

            var tasks = new Task[]
            {
                Task.Run(() => {
                        var commonData = new CommonData(dbContext.CreateDbContext());
                        salutations = commonData.Salutations();
                }),
                Task.Run(() => {
                    var commonData = new CommonData(dbContext.CreateDbContext());
                    nationalities = commonData.GetNationalities();
                }),
                Task.Run(() => {
                    var commonData = new CommonData(dbContext.CreateDbContext());
                    countries = commonData.GetCountries();
                })
            };

            Task.WaitAll(tasks);

            var commonData = new CommonData(dbContext);
            var genders = commonData.GetGender();

            var profdef = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.CommonPayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.CommonPayloads.Nationality>>(nationalities),
                mapper.Map<List<Payloads.CommonPayloads.Gender>>(genders),
                mapper.Map<List<Payloads.CommonPayloads.Country>>(countries));

            var result = new SkyResult<ProfileDefinitionResponse>();

            return Results.Ok(result.SucceededWithValue(profdef));

        }
    }
}
