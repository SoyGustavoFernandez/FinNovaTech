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
    public class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, ResponseDTO<RoleDTO>>
    {
        private readonly IRoleRepository _repository;

        public GetRoleByIdHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<RoleDTO>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new ResponseDTO<RoleDTO>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            var roleDTO = new RoleDTO { Name = role.Name };
            return new ResponseDTO<RoleDTO>(true, "Rol encontrado", roleDTO, (int)HttpStatusCode.OK);
        }
    }
}