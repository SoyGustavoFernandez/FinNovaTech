using FinNovaTech.Common;
using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Users.Handler
{
    /// <summary>
    /// Handler para actualizar la contraseña de un usuario.
    /// </summary>
    public class UpdateUserPasswordHandler(IUserRepository repository, IUserLogRepository userLogRepository, Argon2Hasher argon2Hasher) : IRequestHandler<UpdateUserPasswordCommand, Response<string>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IUserLogRepository _userLogRepository = userLogRepository;
        private readonly Argon2Hasher _argon2Hasher = argon2Hasher;

        public async Task<Response<string>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserEntityByIdAsync(request.Id);
            if (user == null)
            {
                return new Response<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return new Response<string>(false, "La contraseña no puede estar vacía", null, (int)HttpStatusCode.BadRequest);
            }

            user.PasswordHash = _argon2Hasher.HashPassword(request.Password, Guid.NewGuid().ToString());
            await _repository.UpdateUserAsync(user);

            await _userLogRepository.AddLogAsync(new UserLog
            {
                UserId = user.Id,
                Action = "Usuario actualizó contraseña"
            });
            return new Response<string>(true, "Contraseña actualizada correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}