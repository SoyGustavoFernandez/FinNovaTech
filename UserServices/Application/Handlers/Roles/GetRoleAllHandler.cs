using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Roles;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para obtener todos los roles.
    /// </summary>
    public class GetRoleAllHandler : IRequestHandler<GetAllRolesQuery, ResponseDTO<List<RoleDTO>>>
    {
        private readonly IRoleRepository _repository;

        public GetRoleAllHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<List<RoleDTO>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetRolesAsync();
            return new ResponseDTO<List<RoleDTO>>(true, "Roles encontrados", roles, (int)HttpStatusCode.OK);
        }
    }
}