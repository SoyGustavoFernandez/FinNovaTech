using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using FinNovaTech.User.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.User.Infrastructure.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<List<RoleDto>> GetRolesAsync()
        {
            return await _context.Roles.Select(r => new RoleDto
            {
                Name = r.Name,
            }).ToListAsync();
        }

        public async Task UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}