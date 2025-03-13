using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Infrastructure.Data;
using rolEntity = UserService.Domain.Entities;

namespace UserService.Application.Handlers.Roles
{
    /// <summary>
    /// Handler para registrar un rol.
    /// </summary>
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, ResponseDTO<string>>
    {
        private readonly ApplicationDbContext _context;

        public CreateRoleHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            rolEntity.Roles role = new()
            {
                Name = request.Name
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return new ResponseDTO<string>(true, "Rol registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}