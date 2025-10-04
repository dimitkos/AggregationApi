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

            var existingCommentIds = context.Comments
                .Select(comment => comment.Id)
                .ToHashSet();

            var existingRecipeIds = context.Recipes
                .Select(recipe => recipe.Id)
                .ToHashSet();

            var newComments = aggregates.Comments
                .Where(c => !existingCommentIds.Contains(c.Id))
                .ToList();

            var newRecipes = aggregates.Recipes
                .Where(r => !existingRecipeIds.Contains(r.Id))
                .ToList();

            context.Comments.AddRange(aggregates.Comments);
            context.Recipes.AddRange(aggregates.Recipes);
            context.Weather.Add(aggregates.Weather);

            await context.SaveChangesAsync();
        }
    }
}
