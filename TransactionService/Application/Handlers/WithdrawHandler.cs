using MediatR;
using System.Net;
using TransactionService.Application.Command;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, ResponseDTO<string>>
    {
        private readonly ITransactionEventStore _eventStore;

        public WithdrawHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ResponseDTO<string>> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetEventsByAccountIdAsync(request.AccountId);
            if (events == null)
            {
                return new ResponseDTO<string>(false, "No se encontraron eventos para la cuenta", null, (int)HttpStatusCode.NotFound);
            }
            var transaction = new Domain.Entities.TransactionEventEntity
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionTypeEnum.Withdraw.ToString()
            };

            await _eventStore.SaveAsync(transaction);
            return new ResponseDTO<string>(true, "Retiro realizado con éxito", null, (int)HttpStatusCode.OK);
        }
    }
}