using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, HttpStatusCode>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserValidation _userService;

        public RegisterUserHandler(ApplicationDbContext context, IUserValidation userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<HttpStatusCode> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!await _userService.ValidateUserEmailAsync(request.Email))
                {
                    return HttpStatusCode.BadRequest;
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
                return HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
