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
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserValidation _userService;

        public RegisterUserHandler(ApplicationDbContext context, IUserValidation userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<ResponseDTO<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ResponseDTO<string>(false, "Error al registrar usuario", null, (int)HttpStatusCode.BadRequest);
            }
        }
    }
}
