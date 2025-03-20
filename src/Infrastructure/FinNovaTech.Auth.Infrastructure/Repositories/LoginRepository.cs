using FinNovaTech.Auth.Application.Interfaces;
using FinNovaTech.Auth.Domain.Entities;
using FinNovaTech.Auth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Auth.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationDbContext _context;

        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            await _context.AuthUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.AuthUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}