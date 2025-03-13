using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly ApplicationDbContext _context;

        public UserLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(UserLogs log)
        {
            _context.UserLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserLogs>> GetLogsByUserIdAsync(int userId)
        {
            return await _context.UserLogs.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).AsNoTracking().ToListAsync();
        }
    }
}   