using TransactionService.Domain.Enums;

namespace TransactionService.Application.DTOs
{
    public class TransactionEventDTO
    {
        public Guid Id { get; set; }
        public int AccountId { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public TransactionEventDTO(int accountId, TransactionTypeEnum type, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Type = type;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }
    }
}