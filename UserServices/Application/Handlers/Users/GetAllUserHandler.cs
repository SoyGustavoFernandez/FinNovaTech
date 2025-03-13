using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Queries.Users;
using UserService.Infrastructure.Data;

namespace UserService.Application.Handlers.Users
{
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, ResponseDTO<List<UserDTO>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllUserHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Select(x => new UserDTO { Name = x.Name, Email = x.Email }).ToListAsync();
            return new ResponseDTO<List<UserDTO>>(true, "Usuarios encontrados", users, (int)HttpStatusCode.OK);
        }
    }
}