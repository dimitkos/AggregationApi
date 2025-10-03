using Domain.Aggregates;

namespace Application.Services.Infrastructure
{
    public interface IUserPersistence
    {
        Task AddUser(User user);
    }
}
