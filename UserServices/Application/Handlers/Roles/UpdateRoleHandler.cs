using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para actualizar un rol.
    /// </summary>
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateRoleHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(request.Id);
            if (role == null)
            {
                return new ResponseDTO<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            role.Name = request.Name;
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Rol actualizado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}