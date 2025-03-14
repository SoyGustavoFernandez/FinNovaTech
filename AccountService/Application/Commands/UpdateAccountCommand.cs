using AccountService.Application.DTOs;
using AccountService.Domain.Enums;
using MediatR;

namespace AccountService.Application.Commands
{
    public class UpdateAccountCommand : IRequest<ResponseDTO<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public AccountTypeEnum AccountType { get; set; }

        public UpdateAccountCommand(int id, int userId, AccountTypeEnum accountType)
        {
            Id = id;
            UserId = userId;
            AccountType = accountType;
        }
    }
}