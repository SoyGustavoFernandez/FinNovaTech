﻿using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Commands.Roles.Handlers
{
    /// <summary>
    /// Handler para eliminar un rol.
    /// </summary>
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, ResponseDto<string>>
    {
        private readonly IRoleRepository _repository;

        public DeleteRoleHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return new ResponseDto<string>(false, "Rol no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteRoleAsync(role);
            return new ResponseDto<string>(true, "Rol eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}