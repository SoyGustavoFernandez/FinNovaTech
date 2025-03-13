using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para actualizar un usuario.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserValidation _userValidation;

        public UpdateUserHandler(ApplicationDbContext context, IUserValidation userValidation)
        {
            _context = context;
            _userValidation = userValidation;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            bool validEmail = await _userValidation.ValidateUserFormatEmailAsync(request.Email);
            if (!validEmail)
            {
                return new ResponseDTO<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }
            var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists)
            {
                return new ResponseDTO<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = request.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Usuario actualizado", null, (int)HttpStatusCode.OK);
        }
    }
}