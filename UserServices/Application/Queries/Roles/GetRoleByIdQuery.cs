using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Roles
{
    /// <summary>
    /// Query para obtener un rol por su id
    /// </summary>
    public class GetRoleByIdQuery : IRequest<ResponseDTO<RoleDTO>>
    {
        /// <summary>
        /// Identificador del rol
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}