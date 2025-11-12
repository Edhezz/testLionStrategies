namespace LionStrategiesTest.DTOs.Declarations
{
    public class DeclarationDto
    {
        public Guid Id { get; set; }
        public string Month { get; set; } = string.Empty;
        public decimal SalesVat { get; set; }
        public decimal PurchasesVat { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}