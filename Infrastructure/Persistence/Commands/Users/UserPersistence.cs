using Application.Services.Infrastructure;
using Domain.Aggregates;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Commands.Users
{
    class UserPersistence : IUserPersistence
    {
        private readonly DbContextOptions<AggreegationDbContext> _options;

        public UserPersistence(DbContextOptions<AggreegationDbContext> options)
        {
            _options = options;
        }

        public async Task AddUser(User user)
        {
            using var context = new AggreegationDbContext(_options);

            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
