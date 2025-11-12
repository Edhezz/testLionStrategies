using LionStrategiesTest.Data;
using LionStrategiesTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LionStrategiesTest.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationDbContext _context;

        public OperationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Operation> CreateAsync(Operation operation)
        {
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
            return operation;
        }

        public async Task DeleteAsync(Operation operation)
        {
            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Operation>> GetAllAsync(string userEmail, string userRole)
        {
            if (userRole == "admin")
            {
                return await _context.Operations.Include(op => op.User).ToListAsync();
            }

            return await _context.Operations
                .Include(op => op.User) 
                .Where(op => op.User.Email == userEmail)
                .ToListAsync();
        }

        public async Task<Operation?> GetByIdAsync(Guid id)
        {
            return await _context.Operations.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Operation>> GetPurchasesAsync()
        {
            return await _context.Operations.Where(o => o.Type == "purchase").ToListAsync();
        }

        public async Task<IEnumerable<Operation>> GetSalesAsync()
        {
            return await _context.Operations.Where(o => o.Type == "sale").ToListAsync();
        }

        public async Task UpdateAsync(Operation operation)
        {
            _context.Operations.Update(operation);
            await _context.SaveChangesAsync();
        }
    }
}