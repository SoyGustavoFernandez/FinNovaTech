using FinNovaTech.Transaction.Domain.Enums;

namespace FinNovaTech.Transaction.Application.DTOs
{
    public class TransactionEventDto(int accountId, TransactionType type, decimal amount)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int AccountId { get; set; } = accountId;
        public TransactionType Type { get; set; } = type;
        public decimal Amount { get; set; } = amount;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}