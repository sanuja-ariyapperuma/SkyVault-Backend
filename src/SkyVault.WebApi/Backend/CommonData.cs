using Microsoft.EntityFrameworkCore;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public sealed class CommonData
    {
        private readonly SkyvaultContext _db;

        public CommonData(SkyvaultContext db)
        {
            _db = db;
        }

        public List<Salutation> Salutations()
        {
            return _db.Salutations.AsNoTracking().ToList();
        }

        public Salutation? Salutation(int salutationId)
        {
            return _db.Salutations.Find(salutationId);
        }

        public List<Country> GetCountries()
        {
            return _db.Countries.AsNoTracking().ToList();
        }

        public Country? GetCountry(int countryId)
        {
            return _db.Countries.Find(countryId);
        }

        public List<Nationality> GetNationalities()
        {
            return _db.Nationalities.AsNoTracking().ToList();
        }

        public Nationality? GetNationality(int nationalityId)
        {
            return _db.Nationalities.Find(nationalityId);
        }

        public List<CommunicationMethod> GetCommunicationMethods()
        {
            return _db.CommunicationMethods.AsNoTracking().ToList();
        }

        public CommunicationMethod? GetCommunicationMethod(int comId)
        {
            return _db.CommunicationMethods.Find(comId);
        }

        public List<Gender> GetGender() 
        {
            return new List<Gender>() {
                new Gender(){Id = "M", Name = "Male"},
                new Gender(){Id = "F", Name = "Female"}
            };
        }


    }
}
