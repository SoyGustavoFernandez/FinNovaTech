using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Roles
{
    /// <summary>
    /// Comando para registrar un rol.
    /// </summary>
    public class CreateRoleCommand : IRequest<ResponseDTO<string>>
    {
        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        public CreateRoleCommand(string name)
        {
            Name = name;
        }
    }
}