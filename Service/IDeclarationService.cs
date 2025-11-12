using LionStrategiesTest.DTOs.Declarations;

namespace LionStrategiesTest.Services
{
    public interface IDeclarationService
    {
        Task<DeclarationDto> GenerateDeclarationAsync(int year, int month);
        Task<DeclarationDto?> GetDeclarationAsync(int year, int month);
        Task<string?> GetDeclarationStatusAsync(int year, int month);
        Task<bool> UpdateDeclarationStatusAsync(int year, int month, string newStatus);
        Task<bool> DeleteDeclarationAsync(int year, int month);
    }
}