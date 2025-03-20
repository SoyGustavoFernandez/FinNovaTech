using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using FinNovaTech.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.User.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly ApplicationDbContext _context;

        public UserLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(UserLog log)
        {
            _context.UserLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserLog>> GetLogsByUserIdAsync(int userId)
        {
            return await _context.UserLogs.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).AsNoTracking().ToListAsync();
        }
    }
}