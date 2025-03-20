using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using MediatR;

namespace FinNovaTech.User.Application.Queries.Roles
{
    /// <summary>
    /// Query para obtener todos los roles.
    /// </summary>
    public class GetAllRolesQuery : IRequest<Response<List<RoleDto>>>
    {
    }
}