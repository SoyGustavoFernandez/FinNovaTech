namespace TransactionService.Application.Interfaces
{
    public interface ITransactionServices
    {
        Task SaveTransactionAsync(int accountId, decimal amount, string type);
        Task<decimal> GetAccountBalanceAsync(int accountId);
    }
}