using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionStrategiesTest.Models
{
    public class Declaration
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(7)]
        public string Month { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesVat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasesVat { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public string Status { get; set; } 

        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Propiedad de navegación: Una declaración puede tener muchas operaciones
        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}