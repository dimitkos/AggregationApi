using System.Text.Json.Serialization;

namespace Shared
{
    public class AggregationModel
    {
        public List<CommentModel> Comments { get; }
        public List<RecipeModel> Recipes { get; }

        public AggregationModel(List<CommentModel> comments, List<RecipeModel> recipes)
        {
            Comments = comments;
            Recipes = recipes;
        }
    }

    public class CommentModel
    {
        public int Id { get; }
        public int PostId { get; }
        public string Name { get; }
        public string Email { get; }
        public string Body { get; }

        public CommentModel(int id, int postId, string name, string email, string body)
        {
            Id = id;
            PostId = postId;
            Name = name;
            Email = email;
            Body = body;
        }
    }

    public class RecipesResponse
    {
        public List<RecipeModel> Recipes { get;}

        [JsonConstructor]
        public RecipesResponse(List<RecipeModel> recipes)
        {
            Recipes = recipes;
        }
    }

    public class RecipeModel
    {
        public int Id { get; }
        public string Name { get; }
        public int PrepTimeMinutes { get; }
        public int CookTimeMinutes { get; }
        public string Difficulty { get; }
        public string Cuisine { get; }
        public int CaloriesPerServing { get; }

        public RecipeModel(int id, string name, int prepTimeMinutes, int cookTimeMinutes, string difficulty, string cuisine, int caloriesPerServing)
        {
            Id = id;
            Name = name;
            PrepTimeMinutes = prepTimeMinutes;
            CookTimeMinutes = cookTimeMinutes;
            Difficulty = difficulty;
            Cuisine = cuisine;
            CaloriesPerServing = caloriesPerServing;
        }
    }

    public class StatisticsModel
    {
        public int TotalRequests { get; }
        public double TotalResponseTimeMs { get; }
        public int FastCount { get; }
        public int AverageCount { get; }
        public int SlowCount { get; }
        public double AverageResponseTime { get; }

        public StatisticsModel(int totalRequests, double totalResponseTimeMs, int fastCount, int averageCount, int slowCount, double averageResponseTime)
        {
            TotalRequests = totalRequests;
            TotalResponseTimeMs = totalResponseTimeMs;
            FastCount = fastCount;
            AverageCount = averageCount;
            SlowCount = slowCount;
            AverageResponseTime = averageResponseTime;
        }
    }
}
