using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SkyVault.WebApi.Backend.Models;

namespace SkyVault.WebApi.Backend
{
    public class CacheService
    {
        private readonly IMemoryCache _cache;
        private readonly CommonData _commonData;
        private readonly SystemUserData _systemUserData;
        private readonly ILogger<CacheService> _logger;

        public CacheService(IMemoryCache cache, SkyvaultContext skyvaultContext, ILogger<CacheService> logger)
        {
            _cache = cache;
            _commonData = new CommonData(skyvaultContext);
            _systemUserData = new SystemUserData(skyvaultContext);
            _logger = logger;
        }

        public List<Salutation> GetSalutations()
        {
            const string key = "Salutations";
            if (!_cache.TryGetValue(key, out List<Salutation> salutations))
            {
                salutations = _commonData.GetSalutations();
                if (salutations.Count > 0)
                {
                    SetLongTermCache(key, salutations);
                    _logger.LogDebug("Cached {Count} salutations", salutations.Count);
                }
            }
            return salutations;
        }

        public List<Country> GetCountries()
        {
            const string key = "Countries";
            if (!_cache.TryGetValue(key, out List<Country> countries))
            {
                countries = _commonData.GetCountries();
                if (countries.Count > 0)
                {
                    SetLongTermCache(key, countries);
                    _logger.LogDebug("Cached {Count} countries", countries.Count);
                }
            }
            return countries;
        }

        public List<Nationality> GetNationalities()
        {
            const string key = "Nationalities";
            if (!_cache.TryGetValue(key, out List<Nationality> nationalities))
            {
                nationalities = _commonData.GetNationalities();
                if (nationalities.Count > 0)
                {
                    SetLongTermCache(key, nationalities);
                    _logger.LogDebug("Cached {Count} nationalities", nationalities.Count);
                }
            }
            return nationalities;
        }

        public SkyResult<string> GetUserRole(string upn)
        {
            const string key = "UserRoles";
            if (!_cache.TryGetValue(key, out Dictionary<string, string> userRoles))
            {
                userRoles = new Dictionary<string, string>();
                SetShortCache(key, userRoles);
            }

            if (userRoles!.TryGetValue(upn, out string role))
                return new SkyResult<string>().SucceededWithValue(role);

            var userRole = _systemUserData.GetUserRoleByUpn(upn, null);

            if (userRole.Succeeded)
            {
                userRoles[upn] = userRole.Value!;
                SetShortCache(key, userRoles);
                _logger.LogDebug("Cached role for UPN: {Upn}", upn);
            }

            return userRole;
        }

        public List<Gender> GetGender() => new List<Gender>() {
                new Gender(){Id = "M", Name = "Male"},
                new Gender(){Id = "F", Name = "Female"}
        };

        /// <summary>
        /// Clears all cache entries. Useful for admin operations or testing.
        /// </summary>
        public void ClearCache()
        {
            if (_cache is MemoryCache memoryCache)
            {
                memoryCache.Compact(1.0); // 100% compaction = clear everything
                _logger.LogInformation("Cache cleared manually");
            }
        }

        /// <summary>
        /// Removes a specific cache entry by key.
        /// </summary>
        public void Remove(string key)
        {
            _cache.Remove(key);
            _logger.LogDebug("Removed cache entry: {Key}", key);
        }

        private void SetLongTermCache(string key, object data)
        {
            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.Normal,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7), // 1 week
                SlidingExpiration = TimeSpan.FromHours(1), // Reset if accessed within 1 hour
                Size = 1 // Count as 1 entry toward SizeLimit
            };

            _cache.Set(key, data, options);
        }

        private void SetShortCache(string key, object data)
        {
            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.Low,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // 10 minutes
                SlidingExpiration = TimeSpan.FromMinutes(2), // Reset if accessed within 2 minutes
                Size = 1
            };

            _cache.Set(key, data, options);
        }
    }
}
