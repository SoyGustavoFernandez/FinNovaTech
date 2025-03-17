using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Roles
{
    /// <summary>
    /// Comando para actualizar un rol.
    /// </summary>
    public class UpdateRoleCommand : IRequest<ResponseDto<string>>
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public UpdateRoleCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
