using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shared;

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

            context.Comments.AddRange(aggregates.Comments);
            context.Recipes.AddRange(aggregates.Recipes);

            await context.SaveChangesAsync();
        }
    }
}
