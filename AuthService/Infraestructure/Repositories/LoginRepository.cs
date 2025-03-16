using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infraestructure.Repositories
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
            _context.SaveChanges();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.AuthUsers.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}