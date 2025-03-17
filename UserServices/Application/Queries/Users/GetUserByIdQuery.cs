using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Queries.Users
{
    /// <summary>
    /// Query para obtener un usuario por su identificador.
    /// </summary>
    public class GetUserByIdQuery : IRequest<ResponseDto<UserDto>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}