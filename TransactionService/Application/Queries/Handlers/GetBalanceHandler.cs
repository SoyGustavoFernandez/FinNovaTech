using MediatR;
using System.Net;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Application.Queries;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Queries.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, ResponseDto<decimal>>
    {
        private readonly ITransactionEventStore _eventStore;

        public GetBalanceHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ResponseDto<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null || !events.Any())
            {
                return new ResponseDto<decimal>(false, "No se encontraron eventos para la cuenta", 0, (int)HttpStatusCode.NotFound);
            }

            decimal balance = events.Sum(e => Enum.Parse<TransactionType>(e.Type) == TransactionType.Deposit ? e.Amount : -e.Amount);
            return new ResponseDto<decimal>(true, "Balance obtenido con éxito", balance, (int)HttpStatusCode.OK);
        }
    }
}