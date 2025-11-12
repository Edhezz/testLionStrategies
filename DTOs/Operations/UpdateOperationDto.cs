using System.ComponentModel.DataAnnotations;

namespace LionStrategiesTest.DTOs.Operations
{
    public class UpdateOperationDto
    {
        [Required]
        [RegularExpression("^(sale|purchase)$", ErrorMessage = "Type must be 'sale' or 'purchase'")]
        public string Type { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}