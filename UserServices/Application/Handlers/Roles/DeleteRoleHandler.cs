using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para eliminar un rol.
    /// </summary>
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteRoleHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.FindAsync(request.Id);
            if (role == null)
            {
                return new ResponseDTO<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Rol eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}