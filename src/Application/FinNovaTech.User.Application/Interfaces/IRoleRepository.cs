using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Domain.Entities;

namespace FinNovaTech.User.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role);
        Task<Role> GetRoleByIdAsync(int id);
        Task DeleteRoleAsync(Role role);
        Task<List<RoleDto>> GetRolesAsync();
        Task UpdateRole(Role role);
    }
}