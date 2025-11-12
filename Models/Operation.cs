using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionStrategiesTest.Models
{
    public class Operation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        // Claves foráneas
        public Guid UserId { get; set; }
        public Guid? DeclarationId { get; set; }

        // Propiedades de navegación
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("DeclarationId")]
        public Declaration? Declaration { get; set; }
    }
}