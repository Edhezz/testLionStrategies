using LionStrategiesTest.DTOs.Users;

namespace LionStrategiesTest.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<bool> DeleteAsync(Guid id);
    }
}