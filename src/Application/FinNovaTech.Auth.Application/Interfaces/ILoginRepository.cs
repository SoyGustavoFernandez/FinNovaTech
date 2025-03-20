using FinNovaTech.Auth.Domain.Entities;

namespace FinNovaTech.Auth.Application.Interfaces
{
    public interface ILoginRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}