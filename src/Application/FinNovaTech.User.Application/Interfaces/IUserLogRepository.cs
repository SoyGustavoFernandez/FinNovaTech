using FinNovaTech.User.Domain.Entities;

namespace FinNovaTech.User.Application.Interfaces
{
    public interface IUserLogRepository
    {
        Task AddLogAsync(UserLog log);
        Task<List<UserLog>> GetLogsByUserIdAsync(int userId);
    }
}