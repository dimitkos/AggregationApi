using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Commands.Aggregates
{
    class AggregatesPersistence : IAggregatesPersistence
    {
        private readonly DbContextOptions<AggreegationDbContext> _options;

        public AggregatesPersistence(DbContextOptions<AggreegationDbContext> options)
        {
            _options = options;
        }

        public async Task StoreAllAggregates(AggregationEntity aggregates)
        {
            using var context = new AggreegationDbContext(_options);

            //just in our case with more static data and with no high volume of records in the database
            var existingCommentIds = context.Comments
                .Select(comment => comment.Id)
                .ToHashSet();

            var existingRecipeIds = context.Recipes
                .Select(recipe => recipe.Id)
                .ToHashSet();

            var newComments = aggregates.Comments
                .Where(comment => !existingCommentIds.Contains(comment.Id))
                .ToList();

            var newRecipes = aggregates.Recipes
                .Where(recipe => !existingRecipeIds.Contains(recipe.Id))
                .ToList();

            context.Comments.AddRange(aggregates.Comments);
            context.Recipes.AddRange(aggregates.Recipes);
            context.Weather.Add(aggregates.Weather);

            await context.SaveChangesAsync();
        }
    }
}
