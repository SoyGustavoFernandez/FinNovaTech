﻿using MediatR;
using System.Net;
using UserService.Application.Commands.Roles;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using rolEntity = UserService.Domain.Entities;

namespace UserService.Application.Commands.Roles.Handlers
{
    /// <summary>
    /// Handler para registrar un rol.
    /// </summary>
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, ResponseDto<string>>
    {
        private readonly IRoleRepository _repository;

        public CreateRoleHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            rolEntity.Roles role = new()
            {
                Name = request.Name
            };
            await _repository.AddRoleAsync(role);
            return new ResponseDto<string>(true, "Rol registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}