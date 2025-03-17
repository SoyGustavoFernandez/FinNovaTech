using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Shared;

namespace UserService.Application.Commands.Users.Handlers
{
    /// <summary>
    /// Handler para actualizar la contraseña de un usuario.
    /// </summary>
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, ResponseDto<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserLogRepository _userLogRepository;
        private readonly Argon2Hasher _argon2Hasher;

        public UpdateUserPasswordHandler(IUserRepository repository, IUserLogRepository userLogRepository, Argon2Hasher argon2Hasher)
        {
            _repository = repository;
            _userLogRepository = userLogRepository;
            _argon2Hasher = argon2Hasher;
        }

        public async Task<ResponseDto<string>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserEntityByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDto<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return new ResponseDto<string>(false, "La contraseña no puede estar vacía", null, (int)HttpStatusCode.BadRequest);
            }

            user.PasswordHash = _argon2Hasher.HashPassword(request.Password, Guid.NewGuid().ToString());
            await _repository.UpdateUserAsync(user);

            await _userLogRepository.AddLogAsync(new UserLogs
            {
                UserId = user.Id,
                Action = "Usuario actualizó contraseña"
            });
            return new ResponseDto<string>(true, "Contraseña actualizada correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}