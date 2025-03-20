using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Queries.Roles.Handler
{
    /// <summary>
    /// Handler para obtener todos los roles.
    /// </summary>
    public class GetRoleAllHandler(IRoleRepository repository) : IRequestHandler<GetAllRolesQuery, Response<List<RoleDto>>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetRolesAsync();
            return new Response<List<RoleDto>>(true, "Roles encontrados", roles, (int)HttpStatusCode.OK);
        }
    }
}