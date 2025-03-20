using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Roles
{
    /// <summary>
    /// Comando para actualizar un rol.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="name"></param>
    public class UpdateRoleCommand(int id, string name) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int Id { get; set; } = id;
        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Name { get; set; } = name;
    }
}