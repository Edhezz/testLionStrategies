using LionStrategiesTest.Data;
using LionStrategiesTest.DTOs.Declarations;
using LionStrategiesTest.Models;
using LionStrategiesTest.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionStrategiesTest.Services
{
    public class DeclarationService : IDeclarationService
    {
        private readonly IDeclarationRepository _declarationRepository;
        private readonly ApplicationDbContext _context; 
        private const decimal VAT_RATE = 0.16m; // IVA

        public DeclarationService(IDeclarationRepository declarationRepository, ApplicationDbContext context)
        {
            _declarationRepository = declarationRepository;
            _context = context;
        }

        public async Task<DeclarationDto> GenerateDeclarationAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1);

            var operationsInMonth = await _context.Operations
                .Where(op => op.Date >= startDate && op.Date < endDate && op.DeclarationId == null) 
                .ToListAsync();
            
            // Calcular el IVA de ventas y compras
            var totalSales = operationsInMonth.Where(op => op.Type == "sale").Sum(op => op.Amount);
            var totalPurchases = operationsInMonth.Where(op => op.Type == "purchase").Sum(op => op.Amount);

            var salesVat = totalSales * VAT_RATE;
            var purchasesVat = totalPurchases * VAT_RATE;
            var balance = salesVat - purchasesVat;

            // Revisar si ya existe una declaración para ese mes
            var existingDeclaration = await _declarationRepository.GetByYearAndMonthAsync(year, month);
            Declaration declaration;

            if (existingDeclaration != null)
            {
                // Si existe, se actualiza (Sobreescribiendo)
                declaration = existingDeclaration;
                declaration.SalesVat = salesVat;
                declaration.PurchasesVat = purchasesVat;
                declaration.Balance = balance;
                declaration.Status = "pending"; 
                declaration.UpdatedDate = DateTime.UtcNow;
                await _declarationRepository.UpdateAsync(declaration);
            }
            else
            {
                // Si no existe, crea una nueva
                declaration = new Declaration
                {
                    Id = Guid.NewGuid(),
                    Month = $"{year:D4}-{month:D2}",
                    SalesVat = salesVat,
                    PurchasesVat = purchasesVat,
                    Balance = balance,
                    Status = "pending",
                    CreationDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                await _declarationRepository.CreateAsync(declaration);
            }

            // Asociar todas las operaciones procesadas a la nueva declaración
            foreach (var op in operationsInMonth)
            {
                op.DeclarationId = declaration.Id;
            }
            await _context.SaveChangesAsync(); 
            return MapToDto(declaration);
        }

        public async Task<DeclarationDto?> GetDeclarationAsync(int year, int month)
        {
            var declaration = await _declarationRepository.GetByYearAndMonthAsync(year, month);
            return declaration == null ? null : MapToDto(declaration);
        }

        public async Task<string?> GetDeclarationStatusAsync(int year, int month)
        {
            var declaration = await _declarationRepository.GetByYearAndMonthAsync(year, month);
            return declaration?.Status;
        }

        public async Task<bool> UpdateDeclarationStatusAsync(int year, int month, string newStatus)
        {
            var declaration = await _declarationRepository.GetByYearAndMonthAsync(year, month);
            if (declaration == null) return false;

            declaration.Status = newStatus;
            declaration.UpdatedDate = DateTime.UtcNow;
            await _declarationRepository.UpdateAsync(declaration);
            return true;
        }

         public async Task<bool> DeleteDeclarationAsync(int year, int month)
        {
            var declaration = await _declarationRepository.GetByYearAndMonthAsync(year, month);
            if (declaration == null) return false;

            var operationsToUnlink = await _context.Operations
                .Where(op => op.DeclarationId == declaration.Id)
                .ToListAsync();
            
            foreach (var op in operationsToUnlink)
            {
                op.DeclarationId = null;
            }
            await _context.SaveChangesAsync();

            await _declarationRepository.DeleteAsync(declaration);
            return true;
        }

        private DeclarationDto MapToDto(Declaration declaration)
        {
            return new DeclarationDto
            {
                Id = declaration.Id,
                Month = declaration.Month,
                SalesVat = declaration.SalesVat,
                PurchasesVat = declaration.PurchasesVat,
                Balance = declaration.Balance,
                Status = declaration.Status,
                CreationDate = declaration.CreationDate,
                UpdatedDate = declaration.UpdatedDate
            };
        }
    }
}