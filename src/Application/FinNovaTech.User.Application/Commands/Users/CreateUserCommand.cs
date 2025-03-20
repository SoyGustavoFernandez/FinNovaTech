using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Users
{
    /// <summary>
    /// Comando para registrar un usuario.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="name"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="role"></param>
    public class CreateUserCommand(string name, string email, string password, int role) : IRequest<Response<string>>
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; } = name;
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = email;
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; } = password;
        /// <summary>
        /// Rol del usuario.
        /// </summary>
        public int Role { get; set; } = role;
    }
}