using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PaymentSense.BusinessLayer.Constants;
using System;

namespace PaymentSense.BusinessLayer
{
    public class CacheManager : ICacheManager
    {
        private const string EmptyKey = "EmptyKey";
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheManager> _logger;


        public CacheManager(IMemoryCache cache, ILogger<CacheManager> logger)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void RemoveCache(string cacheKey)
        {
            try
            {
                _logger.LogInformation(LogEventNames.CacheManager.Remove.Enter);

                _cache.Remove(cacheKey);

                _logger.LogInformation(LogEventNames.CacheManager.Remove.Exit);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"{LogEventNames.CacheManager.Remove.Exit} - {e.InnerException}");

            }

        }

        public void Set<T>(string cacheKey, T getItemCallBack)
        {
            try
            {
                _logger.LogInformation(LogEventNames.CacheManager.Set.Enter);

                _cache.Set(cacheKey, getItemCallBack);

                _logger.LogInformation(LogEventNames.CacheManager.Set.Exit);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"{LogEventNames.CacheManager.Set.Exit} - {e.InnerException}");
            }


        }

        public T Get<T>(string cacheKey)
        {
            try
            {
                _logger.LogInformation(LogEventNames.CacheManager.Get.Enter);

                var cachedData = _cache.Get<T>(cacheKey);

                _logger.LogInformation(LogEventNames.CacheManager.Get.Exit);

                return cachedData;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"{LogEventNames.CacheManager.Set.Exit} - {e.InnerException}");
                return _cache.Get<T>(EmptyKey);
            }
        }
    }
}
