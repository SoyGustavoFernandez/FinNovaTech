using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Transaction.Application.Queries
{
    public class GetBalanceQuery(int accountId) : IRequest<Response<decimal>>
    {
        public int AccountId { get; set; } = accountId;
    }
}