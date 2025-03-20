using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.Transaction.Application.Interfaces;
using FinNovaTech.Transaction.Domain.Enums;
using MediatR;
using System.Net;

namespace FinNovaTech.Transaction.Application.Commands.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, Response<string>>
    {
        private readonly ITransactionEventStore _eventStore;

        public WithdrawHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Response<string>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null)
            {
                return new Response<string>(false, "No se encontraron eventos para la cuenta", null, (int)HttpStatusCode.NotFound);
            }
            if (request.Amount <= 0)
            {
                return new Response<string>(false, "El monto debe ser mayor a cero", null, (int)HttpStatusCode.BadRequest);
            }
            var transaction = new Domain.Entities.TransactionEvent
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionType.Withdraw.ToString()
            };

            await _eventStore.SaveAsync(transaction);
            return new Response<string>(true, "Retiro realizado con éxito", null, (int)HttpStatusCode.OK);
        }
    }
}