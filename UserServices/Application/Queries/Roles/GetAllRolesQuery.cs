using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Roles
{
    /// <summary>
    /// Query para obtener todos los roles.
    /// </summary>
    public class GetAllRolesQuery : IRequest<ResponseDto<List<RoleDto>>>
    {
    }
}