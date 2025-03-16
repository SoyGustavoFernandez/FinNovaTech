using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}