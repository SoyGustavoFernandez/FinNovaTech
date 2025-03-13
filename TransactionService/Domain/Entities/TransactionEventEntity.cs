namespace TransactionService.Domain.Entities
{
    public class TransactionEventEntity
    { 
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}