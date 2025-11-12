using LionStrategiesTest.Models;

namespace LionStrategiesTest.Repositories
{
    public interface IDeclarationRepository
    {
        Task<Declaration?> GetByYearAndMonthAsync(int year, int month);
        Task<Declaration> CreateAsync(Declaration declaration);
        Task<Declaration> UpdateAsync(Declaration declaration);
        Task DeleteAsync(Declaration declaration);
    }
}