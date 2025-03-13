using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;

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

            await _eventStore.SaveAsync(transactionEvent);
        }

        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(accountId);
            return events.Sum(x => x.Type == "Deposit" ? x.Amount : -x.Amount);
        }
    }
}