using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class CommonData(SkyvaultContext _db)
    {

        public List<Salutation> Salutations() => _db.Salutations.AsNoTracking().ToList();
        public Salutation? Salutation(int salutationId) => _db.Salutations.Find(salutationId);

        public List<Country> GetCountries() => _db.Countries.AsNoTracking().ToList();

        public Country? GetCountry(int countryId) => _db.Countries.Find(countryId);

        public List<Nationality> GetNationalities() => _db.Nationalities.AsNoTracking().ToList();

        public Nationality? GetNationality(int nationalityId) => _db.Nationalities.Find(nationalityId);

        public List<CommunicationMethod> GetCommunicationMethods() => _db.CommunicationMethods.AsNoTracking().ToList();

        public CommunicationMethod? GetCommunicationMethod(int comId) => _db.CommunicationMethods.Find(comId);

        public List<Gender> GetGender() => new List<Gender>() {
                new Gender(){Id = "M", Name = "Male"},
                new Gender(){Id = "F", Name = "Female"}
        };
    }
}
