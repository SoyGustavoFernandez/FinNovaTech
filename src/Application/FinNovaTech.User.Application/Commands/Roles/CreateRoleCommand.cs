using FinNovaTech.Common.Domain.Entities;
using MediatR;

namespace FinNovaTech.User.Application.Commands.Roles
{ /// <summary>
  /// Comando para registrar un rol.
  /// </summary>
  /// <remarks>
  /// Constructor.
  /// </remarks>
  /// <param name="name"></param>
    public class CreateRoleCommand(string name) : IRequest<Response<string>>
    {
        /// <summary>
        /// Nombre del rol.
        /// </summary>
        public string Name { get; set; } = name;
    }
}