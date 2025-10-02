using Application.Services.Infrastructure;
using Common;
using Domain.Aggregates;
using Gateway;
using Gateway.Api;
using MediatR;
using Microsoft.Extensions.Options;
using Shared;

namespace Application.Queries.Aggregates
{
    public class GetAggregatesHandler : IRequestHandler<GetAggregates, AggregationModel>
    {
        private readonly IApiClient<List<CommentModel>> _commentApi;
        private readonly IApiClient<RecipesResponse> _recipeApi;
        private readonly IAggregatesPersistence _persistence;
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
            IAggregatesPersistence persistence,
            IOptions<ApiConfiguration> options)
        {
            //_cache = cache;
            //_cacheSettings = optionsCache.Value;
            //_cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration), AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpiration) };
            _commentApi = commentApi;
            _recipeApi = recipeApi;
            _persistence = persistence;
            _apiConfiguration = options.Value;
        }

        public async Task<AggregationModel?> Handle(GetAggregates request, CancellationToken cancellationToken)
        {
            // 1. Try cache
            //if (_cache.TryGetValue(CacheKey, out AggregationModel? cached))
            //    return Task.FromResult(cached);

            var commentsTask = _commentApi.Get(_apiConfiguration.CommentsUrl, Constants.HttpClients.Comments);
            var recipesTask = _recipeApi.Get(_apiConfiguration.RecipesUrl, Constants.HttpClients.Recipes);

            await Task.WhenAll(commentsTask, recipesTask);

            var comments = commentsTask.Result;
            var recipes = recipesTask.Result;
            AggregationEntity entity = MapTo(comments, recipes);
            await _persistence.StoreAllAggregates(entity);

            //set in the cache for next time
            //_cache.Set(CacheKey, result);

            return new AggregationModel(comments, recipes.Recipes);
        }

#warning make it mapper utils
        private static AggregationEntity MapTo(List<CommentModel> commentsModel, RecipesResponse recipesModel)
        {
            var comments = commentsModel
                .Select(x => new Comment(
                    id: x.Id,
                    postId: x.PostId,
                    name: x.Name,
                    email: x.Email,
                    body: x.Body))
                .ToArray();

            var recipes = recipesModel.Recipes
                .Select(x => new Recipe(
                    id: x.Id,
                    name: x.Name,
                    prepTimeMinutes: x.PrepTimeMinutes,
                    cookTimeMinutes: x.CookTimeMinutes,
                    difficulty: x.Difficulty,
                    cuisine: x.Cuisine,
                    caloriesPerServing: x.CaloriesPerServing))
                .ToArray();

            return new AggregationEntity(comments, recipes);
        }
    }
}
