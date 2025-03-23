using Microsoft.Extensions.Caching.Memory;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly CommonData _commonData;
        private readonly SystemUserData _systemUserData;
        private readonly string _correlationId;

        public CacheService(IMemoryCache cache, SkyvaultContext skyvaultContext)
        {
            _cache = cache;
            _commonData = new CommonData(skyvaultContext);
            _systemUserData = new SystemUserData(skyvaultContext);
            _correlationId = "";
        }

        public List<Salutation> GetSalutations()
        {
            if (!_cache.TryGetValue("Salutations", out List<Salutation> salutations))
            {
                salutations = _commonData.GetSalutations();
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
                countries = _commonData.GetCountries();
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
                nationalities = _commonData.GetNationalities();
                if (nationalities.Count > 0)
                {
                    SetCache("Nationalities", nationalities);
                }
            }
            return nationalities;
        }

        public SkyResult<string> GetUserRole(string upn)
        {
            if (!_cache.TryGetValue("UserRoles", out Dictionary<string, string> userRoles))
            {
                userRoles = new Dictionary<string, string>();
                SetCache("UserRoles", userRoles);
            }

            if (userRoles!.TryGetValue(upn, out string role))
                return new SkyResult<string>().SucceededWithValue(role);

            var userRole = _systemUserData.GetUserRoleByUpn(upn, _correlationId);

            if (userRole.Succeeded)
            {
                userRoles[upn] = userRole.Value!;
                SetShortCache("UserRoles", userRoles);
            }

            return userRole;

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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365 * 100)
            });
        }

        private void SetShortCache(string name, Object data)
        {
            _cache.Set(name, data, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
    }
}
