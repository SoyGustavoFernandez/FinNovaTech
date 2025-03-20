using accountEntity = FinNovaTech.Account.Domain.Entities;
using FinNovaTech.Account.Application.Interfaces;
using FinNovaTech.Account.Application.DTOs;
using FinNovaTech.Account.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Account.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(accountEntity.Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account.Id;
        }

        public async Task<List<AccountDto>> GetAllAccountAsync()
        {
            return await _context.Accounts.Select(account => new AccountDto
            {
                UserId = account.UserId,
                Balance = account.Balance,
                AccountType = account.AccountType
            }).ToListAsync();
        }

        public async Task<AccountDto> GetAccountByIdAsync(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            return new AccountDto
            {
                UserId = account.UserId,
                Balance = account.Balance,
                AccountType = account.AccountType
            };
        }

        public async Task<accountEntity.Account> GetAccountEntityByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task UpdateAccountAsync(accountEntity.Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}