using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para eliminar un rol.
    /// </summary>
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, ResponseDTO<string>>
    {
        private readonly IRoleRepository _repository;

        public DeleteRoleHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new ResponseDTO<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteRoleAsync(role);
            return new ResponseDTO<string>(true, "Rol eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}