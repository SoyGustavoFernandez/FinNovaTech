﻿using MediatR;
using System.Net;
using UserService.Application.DTOs;

namespace UserService.Application.Commands.Users
{
    /// <summary>
    /// Comando para registrar un usuario.
    /// </summary>
    public class RegisterUserCommand : IRequest<ResponseDTO<string>>
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Rol del usuario.
        /// </summary>
        public int Role { get; set; }

        public RegisterUserCommand(string name, string email, string password, int role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}