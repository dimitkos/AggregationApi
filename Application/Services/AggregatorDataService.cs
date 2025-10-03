using Application.Services.Infrastructure;
using Common;
using Gateway;
using Gateway.Api;
using Microsoft.Extensions.Options;
using Shared;

namespace Application.Services
{
    interface IAggregatorDataService
    {
        Task<AggregationModel> Fetch();
    }

    public class AggregatorDataService : IAggregatorDataService
    {
        private readonly IApiClient<List<CommentModel>> _commentApi;
        private readonly IApiClient<RecipesResponse> _recipeApi;
        private readonly IApiClient<WeatherModel> _weatherApi;
        private readonly ApiConfiguration _apiConfiguration;

        public AggregatorDataService(
            ICacheAdapter<string, AggregationModel> cacheAdapter,
            IApiClient<List<CommentModel>> commentApi,
            IApiClient<RecipesResponse> recipeApi,
            IApiClient<WeatherModel> weatherApi,
            IAggregatesPersistence persistence,
            IOptions<ApiConfiguration> options)
        {
            _commentApi = commentApi;
            _recipeApi = recipeApi;
            _weatherApi = weatherApi;
            _apiConfiguration = options.Value;
        }

        public async Task<AggregationModel> Fetch()
        {
            var commentsTask = _commentApi.Get(_apiConfiguration.CommentsUrl, Constants.HttpClients.Comments);
            var recipesTask = _recipeApi.Get(_apiConfiguration.RecipesUrl, Constants.HttpClients.Recipes);
            var weatherTask = _weatherApi.Get(_apiConfiguration.WeatherUrl, Constants.HttpClients.Weather);

            await Task.WhenAll(commentsTask, recipesTask, weatherTask);

            var comments = commentsTask.Result;
            var recipes = recipesTask.Result;
            var weather = weatherTask;

            return new AggregationModel(commentsTask.Result, recipesTask.Result.Recipes, weather.Result);
        }
    }
}
