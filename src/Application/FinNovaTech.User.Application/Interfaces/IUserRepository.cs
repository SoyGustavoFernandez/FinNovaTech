using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Domain.Entities;

namespace FinNovaTech.User.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(Domain.Entities.User user);
        Task UpdateUserAsync(Domain.Entities.User user);
        Task DeleteUserAsync(Domain.Entities.User user);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<Domain.Entities.User> GetUserEntityByIdAsync(int id);
        Task<List<UserDto>> GetAllUsersAsync();
    }
}