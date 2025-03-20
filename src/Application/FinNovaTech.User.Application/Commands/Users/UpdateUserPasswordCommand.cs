using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Users
{
    /// <summary>
    /// Comando para actualizar la contraseña de un usuario.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="password"></param>
    public class UpdateUserPasswordCommand(int id, string password) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; } = id;
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; } = password;
    }
}