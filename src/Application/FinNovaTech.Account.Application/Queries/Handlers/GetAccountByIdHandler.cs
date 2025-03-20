using FinNovaTech.Account.Application.DTOs;
using FinNovaTech.Account.Application.Interfaces;
using FinNovaTech.Common.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.Account.Application.Queries.Handlers
{
    public class GetAccountByIdHandler(IAccountRepository accountRepository) : IRequestHandler<GetAccountByIdQuery, Response<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<Response<AccountDto>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);
            if (account == null)
            {
                return new Response<AccountDto>(false, "Cuenta no encontrada", null, (int)HttpStatusCode.NotFound);
            }
            return new Response<AccountDto>(true, "Cuenta encontrada", account, (int)HttpStatusCode.OK);
        }
    }
}