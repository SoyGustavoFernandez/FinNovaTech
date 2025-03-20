using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Roles
{
    /// <summary>
    /// Comando para eliminar un rol.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    public class DeleteRoleCommand(int id) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int Id { get; set; } = id;
    }
}