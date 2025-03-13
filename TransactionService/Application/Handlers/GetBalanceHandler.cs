using MediatR;
using TransactionService.Application.Interfaces;
using TransactionService.Application.Queries;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, decimal>
    {
        private readonly ITransactionEventStore _eventStore;

        public GetBalanceHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<decimal> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            return events.Sum(e => Enum.Parse<TransactionTypeEnum>(e.Type) == TransactionTypeEnum.Deposit ? e.Amount : -e.Amount);
        }
    }
}
