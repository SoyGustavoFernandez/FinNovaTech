using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Users
{
    /// <summary>
    /// Query para obtener todos los usuarios.
    /// </summary>
    public class GetAllUsersQuery : IRequest<ResponseDto<List<UserDto>>>
    {
    }
}