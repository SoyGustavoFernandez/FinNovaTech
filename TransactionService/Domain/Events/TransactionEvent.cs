namespace TransactionService.Domain.Events
{
    public class TransactionEvent
    {
        public Guid EventId { get; set; }
        public int AccountId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }
}