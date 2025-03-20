namespace FinNovaTech.Transaction.Domain.Entities
{
    public class TransactionEvent
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}