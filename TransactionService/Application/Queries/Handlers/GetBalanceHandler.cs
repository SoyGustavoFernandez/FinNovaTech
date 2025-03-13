using MediatR;
using System.Net;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Application.Queries;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Queries.Handlers
{
    public class GetBalanceHandler : IRequestHandler<GetBalanceQuery, ResponseDTO<decimal>>
    {
        private readonly ITransactionEventStore _eventStore;

        public GetBalanceHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ResponseDTO<decimal>> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null || !events.Any())
            {
                return new ResponseDTO<decimal>(false, "No se encontraron eventos para la cuenta", 0, (int)HttpStatusCode.NotFound);
            }

            decimal balance = events.Sum(e => Enum.Parse<TransactionTypeEnum>(e.Type) == TransactionTypeEnum.Deposit ? e.Amount : -e.Amount);
            return new ResponseDTO<decimal>(true, "Balance obtenido con éxito", balance, (int)HttpStatusCode.OK);
        }
    }
}