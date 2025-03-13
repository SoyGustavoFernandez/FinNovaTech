using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionEventStore _eventStore;

        public TransactionServices(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveTransactionAsync(int accountId, decimal amount, string type)
        {
            var transactionEvent = new TransactionEventEntity
            {
                AccountId = accountId,
                Amount = amount,
                Type = type
            };

            if (amount <= 0)
                throw new InvalidOperationException("El monto debe ser mayor a 0.");

            await _eventStore.SaveAsync(transactionEvent);
        }

        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(accountId);
            if (events == null)
            {
                return 0;
            }
            return events.Sum(e => Enum.Parse<TransactionTypeEnum>(e.Type) == TransactionTypeEnum.Deposit ? e.Amount : -e.Amount);
        }
    }
}