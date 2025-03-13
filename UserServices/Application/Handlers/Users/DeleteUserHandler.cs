using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para eliminar un usuario.
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Usuario eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}