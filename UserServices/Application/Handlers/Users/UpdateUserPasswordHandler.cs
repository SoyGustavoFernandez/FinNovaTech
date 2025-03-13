using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para actualizar la contraseña de un usuario.
    /// </summary>
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserPasswordHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            if(string.IsNullOrEmpty(request.Password))
            {
                return new ResponseDTO<string>(false, "La contraseña no puede estar vacía", null, (int)HttpStatusCode.BadRequest);
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Contraseña actualizada correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}