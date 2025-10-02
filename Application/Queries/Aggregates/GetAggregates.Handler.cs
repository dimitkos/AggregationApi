using Common;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shared;

namespace Application.Queries.Aggregates
{
    public class GetAggregatesHandler : IRequestHandler<GetAggregates, AggregationModel>
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        private readonly CacheSettings _cacheSettings;
        private const string CacheKey = "aggregated_data";

        public GetAggregatesHandler(IMemoryCache cache, MemoryCacheEntryOptions cacheOptions, IOptions<CacheSettings> options)
        {
            _cache = cache;
            _cacheSettings = options.Value;
            _cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpiration) };
        }

        public Task<AggregationModel?> Handle(GetAggregates request, CancellationToken cancellationToken)
        {
            // 1. Try cache
            if (_cache.TryGetValue(CacheKey, out AggregationModel cached))
                return Task.FromResult(cached);

            //other wise get the data from the api

            //store them in the database

            //set in the cache for next time
            _cache.Set(CacheKey, result);
        }
    }
}
