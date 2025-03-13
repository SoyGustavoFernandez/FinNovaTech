using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserLogRepository
    {
        Task AddLogAsync(UserLogs log);
        Task<List<UserLogs>> GetLogsByUserIdAsync(int userId);
    }
}