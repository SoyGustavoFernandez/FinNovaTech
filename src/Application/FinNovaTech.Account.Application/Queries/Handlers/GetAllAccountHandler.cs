using FinNovaTech.Account.Application.DTOs;
using FinNovaTech.Account.Application.Interfaces;
using FinNovaTech.Common.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.Account.Application.Queries.Handlers
{
    /// <summary>
    /// Handler para obtener todas las cuentas.
    /// </summary>
    public class GetAllAccountHandler(IAccountRepository accountRepository) : IRequestHandler<GetAllAccountQuery, Response<List<AccountDto>>>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<Response<List<AccountDto>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllAccountAsync();
            return new Response<List<AccountDto>>(true, "Cuentas encontradas", accounts, (int)HttpStatusCode.OK);
        }

    }
}