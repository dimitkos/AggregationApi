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
        private readonly ICacheAdapter<string, AggregationModel> _cacheAdapter;
        private readonly ApiConfiguration _apiConfiguration;
        private const string CacheKey = "aggregated_data";

        public GetAggregatesHandler(
            ICacheAdapter<string, AggregationModel> cacheAdapter,
            IApiClient<List<CommentModel>> commentApi,
            IApiClient<RecipesResponse> recipeApi,
            IAggregatesPersistence persistence,
            IOptions<ApiConfiguration> options)
        {
            _commentApi = commentApi;
            _recipeApi = recipeApi;
            _persistence = persistence;
            _apiConfiguration = options.Value;
            _cacheAdapter = cacheAdapter;
        }

        public async Task<AggregationModel> Handle(GetAggregates request, CancellationToken cancellationToken)
        {
            var cachedResult = _cacheAdapter.TryGet(CacheKey);

            if (cachedResult is not null)
                return cachedResult;

            var model = await GetData();

            var entity = MapTo(model.Comments, model.Recipes);
            await _persistence.StoreAllAggregates(entity);

            _cacheAdapter.Set(CacheKey, model);

            return model;
        }

        private async Task<AggregationModel> GetData()
        {
            var commentsTask = _commentApi.Get(_apiConfiguration.CommentsUrl, Constants.HttpClients.Comments);
            var recipesTask = _recipeApi.Get(_apiConfiguration.RecipesUrl, Constants.HttpClients.Recipes);

            await Task.WhenAll(commentsTask, recipesTask);

            var comments = commentsTask.Result;
            var recipes = recipesTask.Result;

            return new AggregationModel(commentsTask.Result, recipesTask.Result.Recipes);
        }

#warning make it mapper utils
        private static AggregationEntity MapTo(List<CommentModel> commentsModel, List<RecipeModel> recipesModel)
        {
            var comments = commentsModel
                .Select(x => new Comment(
                    id: x.Id,
                    postId: x.PostId,
                    name: x.Name,
                    email: x.Email,
                    body: x.Body))
                .ToArray();

            var recipes = recipesModel
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
