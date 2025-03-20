using FinNovaTech.Transaction.Application.DTOs;
using FinNovaTech.Transaction.Domain.Entities;

namespace FinNovaTech.Transaction.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TransactionEventDto>> GetBalanceAsync(int idCuenta);
        Task SaveTransactionAsync(TransactionEvent entity);
    }
}