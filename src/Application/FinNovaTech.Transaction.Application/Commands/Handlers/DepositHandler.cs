using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.Transaction.Application.Interfaces;
using FinNovaTech.Transaction.Domain.Entities;
using FinNovaTech.Transaction.Domain.Enums;
using MediatR;
using System.Net;

namespace FinNovaTech.Transaction.Application.Commands.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, Response<string>>
    {
        private readonly ITransactionEventStore _eventStore;

        public DepositHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Response<string>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                return new Response<string>(false, "El monto debe ser mayor a cero", null, (int)HttpStatusCode.BadRequest);
            }
            var transaction = new TransactionEvent
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionType.Deposit.ToString()
            };
            await _eventStore.SaveAsync(transaction);
            return new Response<string>(true, "Depósito realizado con éxito", null, (int)HttpStatusCode.Created);
        }
    }
}