using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.Transaction.Application.Interfaces;
using FinNovaTech.Transaction.Domain.Enums;
using MediatR;
using System.Net;

namespace FinNovaTech.Transaction.Application.Queries.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, Response<decimal>>
    {
        private readonly ITransactionEventStore _eventStore;

        public GetBalanceHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Response<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null || !events.Any())
            {
                return new Response<decimal>(false, "No se encontraron eventos para la cuenta", 0, (int)HttpStatusCode.NotFound);
            }

            decimal balance = events.Sum(e => Enum.Parse<TransactionType>(e.Type) == TransactionType.Deposit ? e.Amount : -e.Amount);
            return new Response<decimal>(true, "Balance obtenido con éxito", balance, (int)HttpStatusCode.OK);
        }
    }
}