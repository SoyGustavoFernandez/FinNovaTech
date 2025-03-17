using AccountService.Application.DTOs;
using AccountService.Domain.Enums;
using MediatR;

namespace AccountService.Application.Commands
{
    public class CreateAccountCommand : IRequest<ResponseDto<int>>
    {
        public int UserId { get; set; }
        public AccountType AccountType { get; set; }

        public CreateAccountCommand(int userId, AccountType accountType)
        {
            UserId = userId;
            AccountType = accountType;
        }
    }
}