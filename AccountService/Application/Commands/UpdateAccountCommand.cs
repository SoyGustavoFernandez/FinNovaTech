using AccountService.Application.DTOs;
using AccountService.Domain.Enums;
using MediatR;

namespace AccountService.Application.Commands
{
    public class UpdateAccountCommand : IRequest<ResponseDto<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AccountType AccountType { get; set; }

        public UpdateAccountCommand(int id, int userId, AccountType accountType)
        {
            Id = id;
            UserId = userId;
            AccountType = accountType;
        }
    }
}