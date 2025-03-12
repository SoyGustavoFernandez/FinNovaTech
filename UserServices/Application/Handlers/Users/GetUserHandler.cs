using MediatR;
using UserService.Application.DTOs;
using UserService.Application.Queries.Users;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class GetUserHandler : IRequestHandler<GetUserQueryById, UserDTO>
    {
        private readonly ApplicationDbContext _context; 

        public GetUserHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> Handle(GetUserQueryById request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);

            if (user == null)
            {
                return null;
            }

            return new UserDTO { Name = user.Name, Email = user.Email };
        }
    }
}