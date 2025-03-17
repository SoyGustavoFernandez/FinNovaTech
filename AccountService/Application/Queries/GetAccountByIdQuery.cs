using AccountService.Application.DTOs;
using AccountService.Domain.Entities;
using MediatR;

namespace AccountService.Application.Queries
{
    public class GetAccountByIdQuery : IRequest<ResponseDto<AccountDto>>
    {
        public int AccountId { get; set; }

        public GetAccountByIdQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
}