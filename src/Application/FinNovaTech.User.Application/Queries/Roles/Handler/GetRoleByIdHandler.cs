using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Queries.Roles.Handler
{
    /// <summary>
    /// Handler para obtener un rol por su Id.
    /// </summary>
    public class GetRoleByIdHandler(IRoleRepository repository) : IRequestHandler<GetRoleByIdQuery, Response<RoleDto>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new Response<RoleDto>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            var RoleDto = new RoleDto { Name = role.Name };
            return new Response<RoleDto>(true, "Rol encontrado", RoleDto, (int)HttpStatusCode.OK);
        }
    }
}