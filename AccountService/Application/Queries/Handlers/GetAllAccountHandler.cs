using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using MediatR;
using System.Net;

namespace AccountService.Application.Queries.Handlers
{
    /// <summary>
    /// Handler para obtener todas las cuentas.
    /// </summary>
    public class GetAllAccountHandler: IRequestHandler<GetAllAccountQuery, ResponseDto<List<AccountDto>>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAllAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResponseDto<List<AccountDto>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.GetAllAccountAsync();
            return new ResponseDto<List<AccountDto>>(true, "Cuentas encontradas", accounts, (int)HttpStatusCode.OK);
        }

    }
}
