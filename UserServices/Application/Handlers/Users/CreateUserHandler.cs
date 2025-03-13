using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para registrar un usuario.
    /// </summary>
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ResponseDTO<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidation _userService;
        private readonly IUserLogRepository _userLogRepository;

        public CreateUserHandler(IUserRepository repository, IUserValidation userService, IUserLogRepository userRepository)
        {
            _repository = repository;
            _userService = userService;
            _userLogRepository = userRepository;
        }

        public async Task<ResponseDTO<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _userService.ValidateUserFormatEmailAsync(request.Email))
            {
                return new ResponseDTO<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }

            var emailExists = await _repository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return new ResponseDTO<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }

            User user = new()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = request.Role
            };

            await _repository.AddUserAsync(user);

            await _userLogRepository.AddLogAsync(new UserLogs
            {
                UserId = user.Id,
                Action = "Usuario registrado",
            });
            return new ResponseDTO<string>(true, "Usuario registrado exitosamente", null, (int)HttpStatusCode.Created);
        }
    }
}