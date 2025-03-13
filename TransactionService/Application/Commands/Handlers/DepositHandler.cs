using MediatR;
using System.Net;
using TransactionService.Application.Command;
using TransactionService.Application.DTOs;
using TransactionService.Application.Interfaces;
using TransactionService.Domain.Entities;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Commands.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, ResponseDTO<string>>
    {
        private readonly ITransactionEventStore _eventStore;

        public DepositHandler(ITransactionEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<ResponseDTO<string>> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
            {
                return new ResponseDTO<string>(false, "El monto debe ser mayor a cero", null, (int)HttpStatusCode.BadRequest);
            }
            var transaction = new TransactionEventEntity
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                Type = TransactionTypeEnum.Deposit.ToString()
            };
            await _eventStore.SaveAsync(transaction);
            return new ResponseDTO<string>(true, "Depósito realizado con éxito", null, (int)HttpStatusCode.Created);
        }
    }
}