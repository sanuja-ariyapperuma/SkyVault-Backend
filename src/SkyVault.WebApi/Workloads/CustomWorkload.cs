using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads
{
    internal static class CustomWorkload
    {
        public static async Task<SkyResult<ProfileDefinitionResponse>> GetProfilePageDefinitionData(SkyvaultContext dbContext, IMapper mapper)
        {

            var salutations = await dbContext.Salutations.ToListAsync();
            var nationalities = await dbContext.Nationalities.ToListAsync();
            var countries = await dbContext.Countries.ToListAsync();

            var genders = new List<Gender>() {
                new Gender(){
                    Id = "M", Name = "Male"
                },
                new Gender(){
                    Id = "F", Name = "Female"
                }
            };

            var profdef = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.ResponsePayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.ResponsePayloads.Nationality>>(nationalities),
                genders,
                mapper.Map<List<Payloads.ResponsePayloads.Country>>(countries));

            var result = new SkyResult<ProfileDefinitionResponse>();

            return result.SucceededWithValue(profdef);

        }   
    }
}
