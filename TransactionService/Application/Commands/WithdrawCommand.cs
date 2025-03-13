using MediatR;

namespace TransactionService.Application.Command
{
    public class WithdrawCommand : IRequest<bool>
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