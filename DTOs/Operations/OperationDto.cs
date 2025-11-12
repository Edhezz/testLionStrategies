namespace LionStrategiesTest.DTOs.Operations
{
    public class OperationDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Date { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid? DeclarationId { get; set; }
    }
}