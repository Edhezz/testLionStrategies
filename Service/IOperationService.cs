using LionStrategiesTest.DTOs.Operations;

namespace LionStrategiesTest.Services
{
    public interface IOperationService
    {
        Task<OperationDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<OperationDto>> GetAllAsync(string userEmail, string userRole);
        Task<IEnumerable<OperationDto>> GetSalesAsync();
        Task<IEnumerable<OperationDto>> GetPurchasesAsync();
        Task<OperationDto> CreateAsync(CreateOperationDto createOperationDto);
        Task<bool> UpdateAsync(Guid id, UpdateOperationDto updateOperationDto);
        Task<bool> DeleteAsync(Guid id);
    }
}