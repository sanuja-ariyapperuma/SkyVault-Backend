using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly SkyvaultContext _skyvaultContext;

        public CacheService(IMemoryCache cache, SkyvaultContext skyvaultContext)
        {
            _cache = cache;
            _skyvaultContext = skyvaultContext;
        }

        public List<Salutation> GetSalutations() 
        {
            if (!_cache.TryGetValue("Salutations", out List<Salutation> salutations))
            {
                salutations = _skyvaultContext.Salutations.AsNoTracking().ToList();
                if (salutations.Count > 0) 
                {
                    SetCache("Salutations", salutations);
                }
            }
            return salutations;
        }

        public List<Country> GetCountries()
        {
            if (!_cache.TryGetValue("Countries", out List<Country> countries))
            {
                countries = _skyvaultContext.Countries.AsNoTracking().ToList();
                if (countries.Count > 0)
                {
                    SetCache("Countries", countries);
                }
            }
            return countries;
        }

        public List<Nationality> GetNationalities()
        {
            if (!_cache.TryGetValue("Nationalities", out List<Nationality> nationalities))
            {
                nationalities = _skyvaultContext.Nationalities.AsNoTracking().ToList();
                if (nationalities.Count > 0)
                {
                    SetCache("Nationalities", nationalities);
                }
            }
            return nationalities;
        }

        public List<Gender> GetGender() => new List<Gender>() {
                new Gender(){Id = "M", Name = "Male"},
                new Gender(){Id = "F", Name = "Female"}
        };

        private void SetCache(string name, Object data) 
        {
            _cache.Set(name, data, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365 * 100) // Example: 100 years
            });
        }
    }
}
