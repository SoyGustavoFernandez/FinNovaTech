using MediatR;
using System.Net;
using TransactionService.Application.Command;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Commands.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, ResponseDto<string>>
    {
        private readonly ITransactionEventStore _eventStore;

        public WithdrawHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ResponseDto<string>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null)
            {
                return new ResponseDto<string>(false, "No se encontraron eventos para la cuenta", null, (int)HttpStatusCode.NotFound);
            }
            if (request.Amount <= 0)
            {
                return new ResponseDto<string>(false, "El monto debe ser mayor a cero", null, (int)HttpStatusCode.BadRequest);
            }
            var transaction = new Domain.Entities.TransactionEventEntity
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionType.Withdraw.ToString()
            };

            await _eventStore.SaveAsync(transaction);
            return new ResponseDto<string>(true, "Retiro realizado con éxito", null, (int)HttpStatusCode.OK);
        }
    }
}