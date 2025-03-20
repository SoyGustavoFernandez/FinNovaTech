using FinNovaTech.Transaction.Domain.Entities;

namespace FinNovaTech.Transaction.Application.Interfaces
{
    public interface ITransactionEventStore
    {
        Task SaveAsync(TransactionEvent transactionEvent);
        Task<IEnumerable<TransactionEvent>> GetEventsByAccountIdAsync(int accountId);
    }
}