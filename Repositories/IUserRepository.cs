using LionStrategiesTest.Models;

namespace LionStrategiesTest.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task DeleteAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}