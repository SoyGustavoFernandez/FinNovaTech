using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Transaction.Application.Commands
{
    public class DepositCommand : IRequest<Response<string>>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }

        public DepositCommand(int accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}