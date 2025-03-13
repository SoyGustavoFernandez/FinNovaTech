using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Queries.Roles;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para obtener todos los roles.
    /// </summary>
    public class GetRoleAllHandler : IRequestHandler<GetAllRolesQuery, ResponseDTO<List<RoleDTO>>>
    {
        private readonly ApplicationDbContext _context;

        public GetRoleAllHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<List<RoleDTO>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _context.Roles.Select(r => new RoleDTO { Name = r.Name }).ToListAsync();
            return new ResponseDTO<List<RoleDTO>>(true, "Roles encontrados", roles, (int)HttpStatusCode.OK);
        }
    }
}