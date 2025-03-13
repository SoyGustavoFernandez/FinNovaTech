using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task DeleteUserAsync(User user);
        Task<List<UserDTO>> GetUsersAsync();
        Task UpdateUser(User user);
    }
}