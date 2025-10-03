namespace Domain.Aggregates
{
    public class AggregationEntity
    {
        public Comment[] Comments { get; }
        public Recipe[] Recipes { get; }
        public Weather Weather { get; }

        public AggregationEntity(Comment[] comments, Recipe[] recipes, Weather weather)
        {
            Comments = comments;
            Recipes = recipes;
            Weather = weather;
        }
    }
}
