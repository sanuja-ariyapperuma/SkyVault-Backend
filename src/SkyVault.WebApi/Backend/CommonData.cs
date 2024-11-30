using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class CommonData(SkyvaultContext _db)
    {

        public List<Salutation> GetSalutations() => _db.Salutations.AsNoTracking().ToList();

        public List<Country> GetCountries() => _db.Countries.AsNoTracking().ToList();

        public List<Nationality> GetNationalities() => _db.Nationalities.AsNoTracking().ToList();

        public List<Gender> GetGender() => new List<Gender>() {
                new Gender(){Id = "M", Name = "Male"},
                new Gender(){Id = "F", Name = "Female"}
        };
    }
}
