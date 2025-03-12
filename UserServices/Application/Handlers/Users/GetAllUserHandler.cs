using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
using UserService.Application.Queries.Users;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllUserHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users.Select(x => new UserDTO { Name = x.Name, Email = x.Email }).ToListAsync();
        }
    }
}