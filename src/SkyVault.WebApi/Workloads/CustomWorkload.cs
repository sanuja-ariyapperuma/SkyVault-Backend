using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads
{
    internal static class CustomWorkload
    {
        public static IResult GetProfilePageDefinitionData(SkyvaultContext dbContext, IMapper mapper)
        {
            var commonData = new CommonData(dbContext);

            var salutations = commonData.Salutations();
            var nationalities = commonData.GetNationalities();
            var countries = commonData.GetCountries();
            var genders = commonData.GetGender();

            var profdef = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.ResponsePayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.ResponsePayloads.Nationality>>(nationalities),
                mapper.Map<List<Payloads.ResponsePayloads.Gender>>(genders),
                mapper.Map<List<Payloads.ResponsePayloads.Country>>(countries));

            var result = new SkyResult<ProfileDefinitionResponse>();

            return Results.Ok(result.SucceededWithValue(profdef));

        }   
    }
}
