using MediatR;

namespace TransactionService.Application.Command
{
    public class DepositCommand : IRequest<bool>
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