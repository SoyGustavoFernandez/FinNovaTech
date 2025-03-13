    namespace TransactionService.Domain.Entities
{
    public class TransactionRequest
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}