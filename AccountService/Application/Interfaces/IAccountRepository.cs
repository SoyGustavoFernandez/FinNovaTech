using AccountService.Application.DTOs;
using AccountService.Domain.Entities;

namespace AccountService.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(Account account);
        Task<List<AccountDto>> GetAllAccountAsync();
        Task<AccountDto> GetAccountByIdAsync(int accountId);
        Task<Account> GetAccountEntityByIdAsync(int accountId);
        Task UpdateAccountAsync(Account account);
    }
}