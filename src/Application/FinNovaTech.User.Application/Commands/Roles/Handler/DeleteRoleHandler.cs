using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Roles.Handler
{
    /// <summary>
    /// Handler para eliminar un rol.
    /// </summary>
    public class DeleteRoleHandler(IRoleRepository repository) : IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new Response<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteRoleAsync(role);
            return new Response<string>(true, "Rol eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}