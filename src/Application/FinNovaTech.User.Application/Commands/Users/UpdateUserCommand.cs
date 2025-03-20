using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Users
{
    /// <summary>
    /// Comando para actualizar un usuario.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="role"></param>
    public class UpdateUserCommand(int id, string name, string email, int role) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; } = id;
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; } = name;
        /// <summary>
        /// Email del usuario.
        /// </summary>
        public string Email { get; set; } = email;
        /// <summary>
        /// Rol del usuario.
        /// </summary>
        public int Role { get; set; } = role;
    }
}