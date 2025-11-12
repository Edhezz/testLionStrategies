using LionStrategiesTest.DTOs.Operations;
using LionStrategiesTest.Models;
using LionStrategiesTest.Repositories;

namespace LionStrategiesTest.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;

        public OperationService(IOperationRepository operationRepository)
        {
            _operationRepository = operationRepository;
        }

        public async Task<OperationDto> CreateAsync(CreateOperationDto createOperationDto)
        {
            
            var operation = new Operation
            {
                Type = createOperationDto.Type,
                Amount = createOperationDto.Amount,
                Date = createOperationDto.Date,
                UserId = createOperationDto.UserId
                // Id, DeclarationId son generados o asignados después
            };

            var newOperation = await _operationRepository.CreateAsync(operation);

            // Mapeamos el resultado de vuelta a un DTO para devolverlo
            return MapToDto(newOperation);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var operation = await _operationRepository.GetByIdAsync(id);
            if (operation == null)
            {
                return false; 
            }

            await _operationRepository.DeleteAsync(operation);
            return true;
        }

        public async Task<IEnumerable<OperationDto>> GetAllAsync(string userEmail, string userRole)
        {
            var operations = await _operationRepository.GetAllAsync(userEmail, userRole);
            // Mapeamos la lista completa a una lista de DTOs
            return operations.Select(MapToDto);
        }

        public async Task<OperationDto?> GetByIdAsync(Guid id)
        {
            var operation = await _operationRepository.GetByIdAsync(id);
            return operation == null ? null : MapToDto(operation);
        }

        public async Task<IEnumerable<OperationDto>> GetPurchasesAsync()
        {
            var operations = await _operationRepository.GetPurchasesAsync();
            return operations.Select(MapToDto);
        }

        public async Task<IEnumerable<OperationDto>> GetSalesAsync()
        {
            var operations = await _operationRepository.GetSalesAsync();
            return operations.Select(MapToDto);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateOperationDto updateOperationDto)
        {
            var operation = await _operationRepository.GetByIdAsync(id);
            if (operation == null)
            {
                return false; 
            }

            // Actualizamos las propiedades de la entidad existente
            operation.Type = updateOperationDto.Type;
            operation.Amount = updateOperationDto.Amount;
            operation.Date = updateOperationDto.Date;

            await _operationRepository.UpdateAsync(operation);
            return true;
        }

        private OperationDto MapToDto(Operation operation)
        {
            return new OperationDto
            {
                Id = operation.Id,
                Type = operation.Type,
                Amount = operation.Amount,
                Date = operation.Date.ToString("dd/MM/yyyy"), 
                UserId = operation.UserId,
                DeclarationId = operation.DeclarationId
            };
        }
    }
}