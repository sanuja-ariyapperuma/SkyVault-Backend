using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkyVault.Payloads.ResponsePayloads;
using SkyVault.WebApi.Backend;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Workloads
{
    internal static class CustomWorkload
    {
        public static async Task<IResult> GetProfilePageDefinitionData(SkyvaultContext dbContext, IMapper mapper,
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
                        using var dbctx = CreateDbContext();
                        var commonData = new CommonData(dbctx);
                        salutations = commonData.Salutations();
                }),
                Task.Run(() => {
                    using var dbctx = CreateDbContext();
                    var commonData = new CommonData(dbctx);
                    nationalities = commonData.GetNationalities();
                }),
                Task.Run(() => {
                    using var dbctx = CreateDbContext();
                    var commonData = new CommonData(dbctx);
                    countries = commonData.GetCountries();
                })
            };

            Task.WaitAll(tasks);

            var commonData = new CommonData(dbContext);
            var genders = commonData.GetGender();

            var profdef = new ProfileDefinitionResponse(
                mapper.Map<List<Payloads.ResponsePayloads.Salutation>>(salutations),
                mapper.Map<List<Payloads.ResponsePayloads.Nationality>>(nationalities),
                mapper.Map<List<Payloads.ResponsePayloads.Gender>>(genders),
                mapper.Map<List<Payloads.ResponsePayloads.Country>>(countries));

            var result = new SkyResult<ProfileDefinitionResponse>();

            return Results.Ok(result.SucceededWithValue(profdef));

        }
        private static SkyvaultContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<SkyvaultContext>();
            builder.UseMySql("Server=ayubdevmysql.southeastasia.cloudapp.azure.com;Database=skyvault;Uid=skyapp;Pwd=2NMc5*3Ee%Kxa^K3;", 
                new MySqlServerVersion(new Version(8, 0))); // Or your preferred database provider
            return new SkyvaultContext(builder.Options);
        }
    }
}
