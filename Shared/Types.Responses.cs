using System.Text.Json.Serialization;

namespace Shared
{
    public class AggregationModel
    {
        public List<CommentModel> Comments { get; }
        public List<RecipeModel> Recipes { get; }
        public WeatherModel Weather { get; }

        public AggregationModel(List<CommentModel> comments, List<RecipeModel> recipes, WeatherModel weather)
        {
            Comments = comments;
            Recipes = recipes;
            Weather = weather;
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
        public List<RecipeModel> Recipes { get; }

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
        public string ApiName { get; }
        public int TotalRequests { get; }
        public double TotalResponseTimeMs { get; }
        public int FastCount { get; }
        public int AverageCount { get; }
        public int SlowCount { get; }
        public double AverageResponseTime { get; }
        public IReadOnlyList<(DateTime Timestamp, double ElapsedMs)> RecentRequests { get; }

        public StatisticsModel(
            string apiName,
            int totalRequests,
            double totalResponseTimeMs,
            int fastCount,
            int averageCount,
            int slowCount,
            double averageResponseTime,
            IReadOnlyList<(DateTime Timestamp, double ElapsedMs)> recentRequests)
        {
            ApiName = apiName;
            TotalRequests = totalRequests;
            TotalResponseTimeMs = totalResponseTimeMs;
            FastCount = fastCount;
            AverageCount = averageCount;
            SlowCount = slowCount;
            AverageResponseTime = averageResponseTime;
            RecentRequests = recentRequests;
        }
    }

    public class WeatherModel
    {
        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("main")]
        public Main Main { get; }

        public double Temp => Main.Temp;
        public double Humidity => Main.Humidity;

        public WeatherModel(string name, Main main)
        {
            Name = name;
            Main = main;
        }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; }
        [JsonPropertyName("humidity")]
        public double Humidity { get; }

        public Main(double temp, double humidity)
        {
            Temp = temp;
            Humidity = humidity;
        }
    }
}
