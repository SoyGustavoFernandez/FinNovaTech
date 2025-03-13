using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Users
{
    public class GetAllUsersQuery : IRequest<ResponseDTO<List<UserDTO>>>
    {
    }
}