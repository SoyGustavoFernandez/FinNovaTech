using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Commands.Roles.Handlers
{
    /// <summary>
    /// Handler para actualizar un rol.
    /// </summary>
    public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, ResponseDto<string>>
    {
        private readonly IRoleRepository _repository;

        public UpdateRoleHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new ResponseDto<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            role.Name = request.Name;
            await _repository.UpdateRole(role);
            return new ResponseDto<string>(true, "Rol actualizado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}