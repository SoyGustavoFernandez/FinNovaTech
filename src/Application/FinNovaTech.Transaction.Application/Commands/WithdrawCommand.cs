using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Transaction.Application.Commands
{
    public class WithdrawCommand : IRequest<Response<string>>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }

        public WithdrawCommand(int accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}