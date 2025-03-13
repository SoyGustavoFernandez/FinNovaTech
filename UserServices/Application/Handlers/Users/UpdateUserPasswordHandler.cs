using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, HttpStatusCode>
    {
        private readonly ApplicationDbContext _context;

        public UpdateUserPasswordHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HttpStatusCode> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }
            if(string.IsNullOrEmpty(request.Password))
            {
                return HttpStatusCode.BadRequest;
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}