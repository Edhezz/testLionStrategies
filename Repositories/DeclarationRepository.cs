using LionStrategiesTest.Data;
using LionStrategiesTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LionStrategiesTest.Repositories
{
    public class DeclarationRepository : IDeclarationRepository
    {
        private readonly ApplicationDbContext _context;

        public DeclarationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Declaration> CreateAsync(Declaration declaration)
        {
            await _context.Declarations.AddAsync(declaration);
            await _context.SaveChangesAsync();
            return declaration;
        }

        public async Task DeleteAsync(Declaration declaration)
        {
            _context.Declarations.Remove(declaration);
            await _context.SaveChangesAsync();
        }

        public async Task<Declaration?> GetByYearAndMonthAsync(int year, int month)
        {
            var monthString = $"{year:D4}-{month:D2}";
            return await _context.Declarations.FirstOrDefaultAsync(d => d.Month == monthString);
        }

        public async Task<Declaration> UpdateAsync(Declaration declaration)
        {
            _context.Declarations.Update(declaration);
            await _context.SaveChangesAsync();
            return declaration;
        }
    }
}