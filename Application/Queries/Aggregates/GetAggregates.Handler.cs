using Common;
using Gateway;
using Gateway.Api;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Shared;

namespace Application.Queries.Aggregates
{
    public class GetAggregatesHandler : IRequestHandler<GetAggregates, AggregationModel>
    {
        private readonly IApiClient<List<CommentModel>> _commentApi;
        private readonly IApiClient<RecipesResponse> _recipeApi;
        private readonly ApiConfiguration _apiConfiguration;
        //private readonly IMemoryCache _cache;
        //private readonly MemoryCacheEntryOptions _cacheOptions;
        //private readonly CacheSettings _cacheSettings;
        private const string CacheKey = "aggregated_data";

        public GetAggregatesHandler(
            //IMemoryCache cache, 
            //MemoryCacheEntryOptions cacheOptions,
            //IOptions<CacheSettings> optionsCache,
            IApiClient<List<CommentModel>> commentApi,
            IApiClient<RecipesResponse> recipeApi,
            IOptions<ApiConfiguration> options)
        {
            //_cache = cache;
            //_cacheSettings = optionsCache.Value;
            //_cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpiration) };
            _commentApi = commentApi;
            _apiConfiguration = options.Value;
            _recipeApi = recipeApi;
        }

        public async Task<AggregationModel?> Handle(GetAggregates request, CancellationToken cancellationToken)
        {
            // 1. Try cache
            //if (_cache.TryGetValue(CacheKey, out AggregationModel? cached))
            //    return Task.FromResult(cached);

            var commentsTask = _commentApi.Get(_apiConfiguration.CommentsUrl, Constants.HttpClients.Comments);
            var recipesTask = _recipeApi.Get(_apiConfiguration.RecipesUrl, Constants.HttpClients.Recipes);


            await Task.WhenAll(commentsTask, recipesTask);

            //other wise get the data from the api

            //store them in the database

            //set in the cache for next time
            //_cache.Set(CacheKey, result);

            return new AggregationModel(commentsTask.Result, recipesTask.Result);
        }
    }
}
