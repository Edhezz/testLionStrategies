using LionStrategiesTest.Models;
using Microsoft.EntityFrameworkCore;

namespace LionStrategiesTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Declaration> Declarations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para las relaciones y nombres de tablas/columnas

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasOne(o => o.User)
                      .WithMany(u => u.Operations)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Cascade); 

                entity.HasOne(o => o.Declaration)
                      .WithMany(d => d.Operations)
                      .HasForeignKey(o => o.DeclarationId)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.SetNull); 
            });
        }
    }
}