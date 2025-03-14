using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using MediatR;
using System.Net;

namespace AccountService.Application.Commands.Handlers
{
    public class UpdateAccountHandler :IRequestHandler<UpdateAccountCommand, ResponseDTO<string>>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountEntityByIdAsync(request.Id);
            if (account == null)
            {
                return new ResponseDTO<string>(false, "Cuenta no encontrada", null, (int)HttpStatusCode.NotFound);
            }

            await _accountRepository.UpdateAccountAsync(account);

            return new ResponseDTO<string>(true, "Cuenta actualizada", null, (int)HttpStatusCode.OK);
        }
    }
}
