using MediatR;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    /// <summary>
    /// Comando para eliminar un usuario.
    /// </summary>
    public class DeleteUserCommand : IRequest<ResponseDto<string>>
    {
        /// <summary>
        /// Identificador del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}