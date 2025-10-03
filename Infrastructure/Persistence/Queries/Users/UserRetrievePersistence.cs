using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Queries.Users
{
    class UserRetrievePersistence : IEntityRetrieval<string, User>
    {
        private readonly DbContextOptions<AggreegationDbContext> _options;

        public UserRetrievePersistence(DbContextOptions<AggreegationDbContext> options)
        {
            _options = options;
        }

        public async Task<User?> TryRetrieve(string key)
        {
            using var context = new AggreegationDbContext(_options);

            var user = await context.Users.FirstOrDefaultAsync(x=> x.Email == key);

            return user;
        }
    }
}
