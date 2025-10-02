using Application.Services;
using Application.Services.Infrastructure;
using Gateway;
using Gateway.Api;
using Microsoft.Extensions.Options;
using Moq;
using Shared;

namespace UnitTests.ApplicationTests.Services
{
    public class AggregatorDataServiceTests
    {
        [Fact]
        public async Task Fetch_ReturnsAggregationModelWithCommentsAndRecipes()
        {
            // Arrange
            var comments = new List<CommentModel> { new CommentModel(1, 1, "name", "email", "body") };
            var recipes = new List<RecipeModel> { new RecipeModel(1, "recipe", 10, 20, "Easy", "Italian", 100) };
            var recipesResponse = new RecipesResponse(recipes);

            var commentApiMock = new Mock<IApiClient<List<CommentModel>>>();
            var recipeApiMock = new Mock<IApiClient<RecipesResponse>>();
            var cacheAdapterMock = new Mock<ICacheAdapter<string, AggregationModel>>();
            var persistenceMock = new Mock<IAggregatesPersistence>();
            var config = new ApiConfiguration { CommentsUrl = "comments", RecipesUrl = "recipes" };
            var optionsMock = new Mock<IOptions<ApiConfiguration>>();
            optionsMock.Setup(o => o.Value).Returns(config);

            commentApiMock.Setup(x => x.Get(config.CommentsUrl, It.IsAny<string>(), null)).ReturnsAsync(comments);
            recipeApiMock.Setup(x => x.Get(config.RecipesUrl, It.IsAny<string>(), null)).ReturnsAsync(recipesResponse);

            var service = new AggregatorDataService(
                cacheAdapterMock.Object,
                commentApiMock.Object,
                recipeApiMock.Object,
                persistenceMock.Object,
                optionsMock.Object);

            // Act
            var result = await service.Fetch();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(comments, result.Comments);
            Assert.Equal(recipes, result.Recipes);
        }

        [Fact]
        public async Task Fetch_ReturnsEmptyLists_WhenApisReturnEmpty()
        {
            // Arrange
            var commentApiMock = new Mock<IApiClient<List<CommentModel>>>();
            var recipeApiMock = new Mock<IApiClient<RecipesResponse>>();
            var cacheAdapterMock = new Mock<ICacheAdapter<string, AggregationModel>>();
            var persistenceMock = new Mock<IAggregatesPersistence>();
            var config = new ApiConfiguration { CommentsUrl = "comments", RecipesUrl = "recipes" };
            var optionsMock = new Mock<IOptions<ApiConfiguration>>();
            optionsMock.Setup(o => o.Value).Returns(config);

            commentApiMock.Setup(x => x.Get(config.CommentsUrl, It.IsAny<string>(), null)).ReturnsAsync(new List<CommentModel>());
            recipeApiMock.Setup(x => x.Get(config.RecipesUrl, It.IsAny<string>(), null)).ReturnsAsync(new RecipesResponse(new List<RecipeModel>()));

            var service = new AggregatorDataService(
                cacheAdapterMock.Object,
                commentApiMock.Object,
                recipeApiMock.Object,
                persistenceMock.Object,
                optionsMock.Object);

            // Act
            var result = await service.Fetch();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Comments);
            Assert.Empty(result.Recipes);
        }
    }
}
