using Application.Services;
using Application.Services.Infrastructure;
using Common;
using Domain.Aggregates;
using MediatR;
using Shared;

namespace Application.Queries.Aggregates
{
    class GetAggregatesHandler : IRequestHandler<GetAggregates, AggregationModel>
    {
        private readonly IAggregatorDataService _aggregator;
        private readonly IAggregatesPersistence _persistence;
        private readonly ICacheAdapter<string, AggregationModel> _cacheAdapter;
        private readonly IIdGenerator _idGenerator;
        private const string CacheKey = "aggregated";

        public GetAggregatesHandler(
            IAggregatorDataService aggregator,
            ICacheAdapter<string, AggregationModel> cacheAdapter,
            IAggregatesPersistence persistence,
            IIdGenerator idGenerator)
        {
            _aggregator = aggregator;
            _cacheAdapter = cacheAdapter;
            _persistence = persistence;
            _idGenerator = idGenerator;
        }

        public async Task<AggregationModel> Handle(GetAggregates request, CancellationToken cancellationToken)
        {
            var cachedResult = _cacheAdapter.TryGet(CacheKey);

            if (cachedResult is not null)
                return cachedResult;

            var model = await _aggregator.Fetch();

            var entity = MapTo(model.Comments, model.Recipes, model.Weather);
            await _persistence.StoreAllAggregates(entity);

            //maybe use a decorator for caching?
            _cacheAdapter.Set(CacheKey, model);

            return model;
        }

#warning make it mapper utils
        private AggregationEntity MapTo(List<CommentModel> commentsModel, List<RecipeModel> recipesModel, WeatherModel weatherModel)
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

            var weather = new Weather(
                id: _idGenerator.GenerateId(),
                name: weatherModel.Name,
                temp: weatherModel.Temp,
                humidity: weatherModel.Humidity);

            return new AggregationEntity(comments, recipes, weather);
        }
    }
}
