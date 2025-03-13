using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Queries.Roles;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para obtener un rol por su Id.
    /// </summary>
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, ResponseDTO<RoleDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetRoleByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<RoleDTO>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(request.Id);
            if (role == null)
            {
                return new ResponseDTO<RoleDTO>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            var roleDTO = new RoleDTO { Name = role.Name };
            return new ResponseDTO<RoleDTO>(true, "Rol encontrado", roleDTO, (int)HttpStatusCode.OK);
        }
    }
}