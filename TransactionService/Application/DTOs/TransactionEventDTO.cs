using TransactionService.Domain.Enums;

namespace TransactionService.Application.DTOs
{
    public class TransactionEventDto
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public TransactionEventDto(int accountId, TransactionType type, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }
    }
}