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
}
