using FinNovaTech.Account.Application.Interfaces;
using FinNovaTech.Common.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.Account.Application.Commands.Handlers
{
    public class UpdateAccountHandler(IAccountRepository accountRepository) : IRequestHandler<UpdateAccountCommand, Response<string>>
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        public async Task<Response<string>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountEntityByIdAsync(request.Id);
            if (account == null)
            {
                return new Response<string>(false, "Cuenta no encontrada", null, (int)HttpStatusCode.NotFound);
            }

            await _accountRepository.UpdateAccountAsync(account);

            return new Response<string>(true, "Cuenta actualizada", null, (int)HttpStatusCode.OK);
        }
    }
}