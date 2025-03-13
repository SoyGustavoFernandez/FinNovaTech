using MediatR;
using TransactionService.Application.DTOs;

namespace TransactionService.Application.Queries
{
    public class GetBalanceQuery : IRequest<ResponseDTO<decimal>>
    {
        public int AccountId { get; set; }

        public GetBalanceQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}