using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using MediatR;

namespace FinNovaTech.User.Application.Queries.Users
{
    /// <summary>
    /// Query para obtener un usuario por su identificador.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    public class GetUserByIdQuery(int id) : IRequest<Response<UserDto>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; } = id;
    }
}