using MediatR;

namespace TransactionService.Application.Queries
{
    public class GetBalanceQuery : IRequest<decimal>
    {
        public int AccountId { get; set; }

        public GetBalanceQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}