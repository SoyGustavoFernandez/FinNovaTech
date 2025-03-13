using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    /// <summary>
    /// Comando para actualizar un usuario.
    /// </summary>
    public class UpdateUserCommand : IRequest<ResponseDTO<string>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email del usuario.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Rol del usuario.
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="role"></param>
        public UpdateUserCommand(int id, string name, string email, int role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }
    }
}