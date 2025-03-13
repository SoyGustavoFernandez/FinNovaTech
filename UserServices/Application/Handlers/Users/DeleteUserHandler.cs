using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, HttpStatusCode>
    {
        private readonly ApplicationDbContext _context;

        public DeleteUserHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HttpStatusCode> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);
            if (user == null)
            {
                return HttpStatusCode.NotFound;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}