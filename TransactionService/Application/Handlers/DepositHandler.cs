using MediatR;
using TransactionService.Application.Command;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, bool>
    {
        private readonly ITransactionEventStore _eventStore;

        public DepositHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<bool> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var transaction = new TransactionEventEntity
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionTypeEnum.Deposit.ToString()
            };
            await _eventStore.SaveAsync(transaction);
            return true;
        }
    }
}