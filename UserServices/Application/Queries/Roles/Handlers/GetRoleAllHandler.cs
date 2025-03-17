using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Roles;

namespace UserService.Application.Queries.Roles.Handlers
{
    /// <summary>
    /// Handler para obtener todos los roles.
    /// </summary>
    public class GetRoleAllHandler : IRequestHandler<GetAllRolesQuery, ResponseDto<List<RoleDto>>>
    {
        private readonly IRoleRepository _repository;

        public GetRoleAllHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetRolesAsync();
            return new ResponseDto<List<RoleDto>>(true, "Roles encontrados", roles, (int)HttpStatusCode.OK);
        }
    }
}