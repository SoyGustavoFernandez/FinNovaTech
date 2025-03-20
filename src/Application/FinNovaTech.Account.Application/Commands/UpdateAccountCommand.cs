using FinNovaTech.Account.Domain.Enums;
using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Account.Application.Commands
{
    public class UpdateAccountCommand(int id, int userId, AccountType accountType) : IRequest<Response<string>>
    {
        public int Id { get; set; } = id;
        public int UserId { get; set; } = userId;
        public AccountType AccountType { get; set; } = accountType;
    }
}