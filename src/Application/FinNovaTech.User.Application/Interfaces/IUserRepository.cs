using FinNovaTech.User.Application.DTOs;
using userEntity = FinNovaTech.User.Domain.Entities;

namespace FinNovaTech.User.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(userEntity.User user);
        Task UpdateUserAsync(userEntity.User user);
        Task DeleteUserAsync(userEntity.User user);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<userEntity.User> GetUserEntityByIdAsync(int id);
        Task<List<UserDto>> GetAllUsersAsync();
    }
}