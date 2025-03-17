using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Roles;

namespace UserService.Application.Queries.Roles.Handlers
{
    /// <summary>
    /// Handler para obtener un rol por su Id.
    /// </summary>
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, ResponseDto<RoleDto>>
    {
        private readonly IRoleRepository _repository;

        public GetRoleByIdHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new ResponseDto<RoleDto>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            var RoleDto = new RoleDto { Name = role.Name };
            return new ResponseDto<RoleDto>(true, "Rol encontrado", RoleDto, (int)HttpStatusCode.OK);
        }
    }
}