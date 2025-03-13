using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Roles
{
    /// <summary>
    /// Comando para eliminar un rol.
    /// </summary>
    public class DeleteRoleCommand: IRequest<ResponseDTO<string>>
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public DeleteRoleCommand(int id)
        {
            Id = id;
        }
    }
}
