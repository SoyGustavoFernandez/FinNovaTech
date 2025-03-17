using AccountService.Application.DTOs;
using AccountService.Application.Interfaces;
using AccountService.Domain.Entities;
using AccountService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Account account)
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

        public async Task<Account> GetAccountEntityByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}