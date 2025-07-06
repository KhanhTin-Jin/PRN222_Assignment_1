using System;

namespace KhanhTin.BusinessLogic.Models
{
    public class CacheResult<T>
    {
        public T Data { get; set; }
        public bool IsFromCache { get; set; }
        public DateTime? CachedAt { get; set; }
        public TimeSpan? CacheAge { get; set; }
        public string CacheKey { get; set; }

        public bool IsExpired(TimeSpan maxAge)
        {
            return CacheAge.HasValue && CacheAge.Value > maxAge;
        }

        public static CacheResult<T> FromCache(T data, DateTime cachedAt, string cacheKey)
        {
            return new CacheResult<T>
            {
                Data = data,
                IsFromCache = true,
                CachedAt = cachedAt,
                CacheAge = DateTime.Now - cachedAt,
                CacheKey = cacheKey
            };
        }

        public static CacheResult<T> FromSource(T data, string cacheKey)
        {
            return new CacheResult<T>
            {
                Data = data,
                IsFromCache = false,
                CacheKey = cacheKey
            };
        }
    }
}
