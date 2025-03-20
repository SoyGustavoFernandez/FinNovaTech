using accountEntity = FinNovaTech.Account.Domain.Entities;
using FinNovaTech.Account.Application.DTOs;

namespace FinNovaTech.Account.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<int> CreateAsync(accountEntity.Account account);
        Task<List<AccountDto>> GetAllAccountAsync();
        Task<AccountDto> GetAccountByIdAsync(int accountId);
        Task<accountEntity.Account> GetAccountEntityByIdAsync(int accountId);
        Task UpdateAccountAsync(accountEntity.Account account);
    }
}