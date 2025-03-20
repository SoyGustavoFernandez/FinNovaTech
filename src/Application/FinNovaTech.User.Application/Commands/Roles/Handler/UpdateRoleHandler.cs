using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Roles.Handler
{
    /// <summary>
    /// Handler para actualizar un rol.
    /// </summary>
    public class UpdateRoleHandler(IRoleRepository repository) : IRequestHandler<UpdateRoleCommand, Response<string>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new Response<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            role.Name = request.Name;
            await _repository.UpdateRole(role);
            return new Response<string>(true, "Rol actualizado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}