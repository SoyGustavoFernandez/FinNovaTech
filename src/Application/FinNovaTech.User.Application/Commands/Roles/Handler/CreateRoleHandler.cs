using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Roles.Handler
{
    /// <summary>
    /// Handler para registrar un rol.
    /// </summary>
    public class CreateRoleHandler(IRoleRepository repository) : IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = new()
            {
                Name = request.Name
            };
            await _repository.AddRoleAsync(role);
            return new Response<string>(true, "Rol registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}