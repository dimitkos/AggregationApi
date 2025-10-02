namespace Domain.Aggregates
{
    public class AggregationEntity
    {
        public Comment[] Comments { get; }
        public Recipe[] Recipes { get; }

        public AggregationEntity(Comment[] comments, Recipe[] recipes)
        {
            Comments = comments;
            Recipes = recipes;
        }
    }
}
