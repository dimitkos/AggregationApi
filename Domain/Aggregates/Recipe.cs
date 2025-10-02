namespace Domain.Aggregates
{
    public class Recipe
    {
        public int Id { get; }
        public string Name { get; }
        public int PrepTimeMinutes { get; }
        public int CookTimeMinutes { get; }
        public string Difficulty { get; }
        public string Cuisine { get; }
        public int CaloriesPerServing { get; }

        public Recipe(int id, string name, int prepTimeMinutes, int cookTimeMinutes, string difficulty, string cuisine, int caloriesPerServing)
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
