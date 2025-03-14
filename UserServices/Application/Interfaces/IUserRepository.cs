using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<User> GetUserEntityByIdAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}