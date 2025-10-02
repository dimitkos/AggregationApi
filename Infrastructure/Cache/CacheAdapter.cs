using Application.Services.Infrastructure;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Infrastructure.Cache
{
    class CacheAdapter<TKey, TEntity> : ICacheAdapter<TKey, TEntity> where TKey : notnull
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly CacheSettings _cacheSettings;

        public CacheAdapter(IMemoryCache cache, IOptions<CacheSettings> options)
        {
            _cache = cache;
            _cacheSettings = options.Value;
            _cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpiration) };
        }

        public TEntity? TryGet(TKey key)
        {
            var tt = ToCacheKey(key);
            if (_cache.TryGetValue(ToCacheKey(key), out var value))
                return (TEntity)value;

            return default;
        }

        public Dictionary<TKey, TEntity> GetMany(TKey[] keys)
        {
            var cacheEntries = new Dictionary<TKey, TEntity>();

            foreach (var key in keys)
            {
                if (!_cache.TryGetValue(ToCacheKey(key), out var value))
                    continue;

                cacheEntries[key] = (TEntity)value;
            }

            return cacheEntries;
        }

        public void Remove(TKey[] keys)
        {
            foreach (var key in keys)
                _cache.Remove(ToCacheKey(key));
        }

        public void Set(TKey key, TEntity value)
            => _cache.Set(ToCacheKey(key), value, _cacheOptions);

        private string ToCacheKey(TKey key)
            => $"{nameof(TKey)}-{key.ToString()}";
    }
}
