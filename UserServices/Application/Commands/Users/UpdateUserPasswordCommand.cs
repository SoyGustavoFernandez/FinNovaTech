using MediatR;
using System.Net;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    /// <summary>
    /// Comando para actualizar la contraseña de un usuario.
    /// </summary>
    public class UpdateUserPasswordCommand: IRequest<ResponseDto<string>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        public UpdateUserPasswordCommand(int id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}
