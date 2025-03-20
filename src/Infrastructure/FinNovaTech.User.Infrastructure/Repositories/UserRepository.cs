using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using FinNovaTech.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.User.Infrastructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddUserAsync(Domain.Entities.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Domain.Entities.User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Domain.Entities.User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
            };
        }

        public async Task<Domain.Entities.User> GetUserEntityByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users.Select(u => new UserDto
            {
                Name = u.Name,
                Email = u.Email,
            }).ToListAsync();
        }
    }
}