using MediatR;
using TransactionService.Application.Command;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, bool>
    {
        private readonly ITransactionEventStore _eventStore;

        public WithdrawHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<bool> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Domain.Entities.TransactionEventEntity
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionTypeEnum.Withdraw.ToString()
            };

            await _eventStore.SaveAsync(transaction);
            return true;
        }
    }
}