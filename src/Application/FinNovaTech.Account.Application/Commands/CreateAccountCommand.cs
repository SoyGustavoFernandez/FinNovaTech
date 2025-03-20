using FinNovaTech.Account.Domain.Enums;
using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.Account.Application.Commands
{
    public class CreateAccountCommand(int userId, AccountType accountType) : IRequest<Response<int>>
    {
        public int UserId { get; set; } = userId;
        public AccountType AccountType { get; set; } = accountType;
    }
}