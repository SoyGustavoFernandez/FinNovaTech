using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(Roles role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Roles role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Roles> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<List<RoleDTO>> GetRolesAsync()
        {
            return await _context.Roles.Select(r => new RoleDTO
            {
                Name = r.Name,
            }).ToListAsync();
        }

        public async Task UpdateRole(Roles role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }
    }
}