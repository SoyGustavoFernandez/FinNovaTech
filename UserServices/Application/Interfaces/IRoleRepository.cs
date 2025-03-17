using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Roles role);
        Task<Roles> GetRoleByIdAsync(int id);
        Task DeleteRoleAsync(Roles role);
        Task<List<RoleDto>> GetRolesAsync();
        Task UpdateRole(Roles role);
    }
}