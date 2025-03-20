using FinNovaTech.Account.Application.DTOs;
using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Account.Application.Queries
{
    public class GetAccountByIdQuery : IRequest<Response<AccountDto>>
    {
        public int AccountId { get; set; }

        public GetAccountByIdQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}