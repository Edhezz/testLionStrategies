using LionStrategiesTest.Models;

namespace LionStrategiesTest.Repositories
{
    public interface IOperationRepository
    {
        Task<Operation?> GetByIdAsync(Guid id);
        Task<IEnumerable<Operation>> GetAllAsync(string userEmail, string userRole);
        Task<IEnumerable<Operation>> GetSalesAsync();
        Task<IEnumerable<Operation>> GetPurchasesAsync();
        Task<Operation> CreateAsync(Operation operation);
        Task UpdateAsync(Operation operation);
        Task DeleteAsync(Operation operation);
    }
}