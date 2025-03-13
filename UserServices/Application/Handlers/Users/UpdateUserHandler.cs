using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, HttpStatusCode>
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserValidation _userValidation;

        public UpdateUserHandler(ApplicationDbContext context, IUserValidation userValidation)
        {
            _context = context;
            _userValidation = userValidation;
        }

        public async Task<HttpStatusCode> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }
            bool validEmail = await _userValidation.ValidateUserEmailAsync(request.Email);
            if (!validEmail)
            {
                return HttpStatusCode.BadRequest;
            }
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = request.Role;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}