using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System.Net;

namespace AccountService.Application.Queries.Handlers
{
    public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, ResponseDto<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountByIdHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResponseDto<AccountDto>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(request.AccountId);
            if (account == null)
            {
                return new ResponseDto<AccountDto>(false, "Cuenta no encontrada", null, (int)HttpStatusCode.NotFound);
            }
            return new ResponseDto<AccountDto>(true, "Cuenta encontrada", account, (int)HttpStatusCode.OK);
        }
    }
}