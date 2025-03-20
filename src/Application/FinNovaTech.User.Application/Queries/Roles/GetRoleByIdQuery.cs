using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using MediatR;

namespace FinNovaTech.User.Application.Queries.Roles
{
    /// <summary>
    /// Query para obtener un rol por su id
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    public class GetRoleByIdQuery(int id) : IRequest<Response<RoleDto>>
    {
        /// <summary>
        /// Identificador del rol
        /// </summary>
        public int Id { get; set; } = id;
    }
}