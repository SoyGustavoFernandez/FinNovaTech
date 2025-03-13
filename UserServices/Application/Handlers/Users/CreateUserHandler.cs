using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para registrar un usuario.
    /// </summary>
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserValidation _userService;

        public CreateUserHandler(ApplicationDbContext context, IUserValidation userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ResponseDTO<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userService.ValidateUserFormatEmailAsync(request.Email))
            {
                return new ResponseDTO<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }

            var emailExists = await _context.Users.AnyAsync(u => u.Email == request.Email);
            if (emailExists)
            {
                return new ResponseDTO<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Usuario registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}