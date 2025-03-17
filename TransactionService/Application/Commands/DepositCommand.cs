using MediatR;
using TransactionService.Application.DTOs;

namespace TransactionService.Application.Command
{
    public class DepositCommand : IRequest<ResponseDto<string>>
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