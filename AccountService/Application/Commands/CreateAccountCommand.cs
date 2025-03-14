using AccountService.Application.DTOs;
using AccountService.Domain.Enums;
using MediatR;

namespace AccountService.Application.Commands
{
    public class CreateAccountCommand : IRequest<ResponseDTO<int>>
    {
        public int UserId { get; set; }
        public AccountTypeEnum AccountType { get; set; }

        public CreateAccountCommand(int userId, AccountTypeEnum accountType)
        {
            UserId = userId;
            AccountType = accountType;
        }
    }
}