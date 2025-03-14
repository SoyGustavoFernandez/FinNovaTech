using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System.Net;

namespace AccountService.Application.Commands.Handlers
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, ResponseDTO<int>>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResponseDTO<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                UserId = request.UserId,
                AccountType = request.AccountType
            };
            await _accountRepository.CreateAsync(account);

            return new ResponseDTO<int>(true, "Cuenta creada correctamente", account.Id, (int)HttpStatusCode.Created);
        }
    }
}