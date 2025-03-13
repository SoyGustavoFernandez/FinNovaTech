using TransactionService.Application.DTOs;
using TransactionService.Domain.Entities;

namespace TransactionService.Application.Interfaces
{
    public interface ITransactionEventStore
    {
        Task SaveAsync(TransactionEventEntity transactionEvent);
        Task<IEnumerable<TransactionEventEntity>> GetEventsByAccountIdAsync(int accountId);
    }
}