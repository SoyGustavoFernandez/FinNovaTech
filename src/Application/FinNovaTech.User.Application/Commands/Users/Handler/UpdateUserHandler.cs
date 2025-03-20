using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Users.Handler
{
    /// <summary>
    /// Handler para actualizar un usuario.
    /// </summary>
    public class UpdateUserHandler(IUserRepository repository, IUserValidation userValidation, IUserLogRepository userLogRepository) : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IUserValidation _userValidation = userValidation;
        private readonly IUserLogRepository _userLogRepository = userLogRepository;

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserEntityByIdAsync(request.Id);
            if (user == null)
            {
                return new Response<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            bool validEmail = await _userValidation.ValidateUserFormatEmailAsync(request.Email);
            if (!validEmail)
            {
                return new Response<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }
            var emailExists = await _repository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return new Response<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = request.Role;
            await _repository.UpdateUserAsync(user);

            await _userLogRepository.AddLogAsync(new UserLog
            {
                UserId = user.Id,
                Action = "Perfil actualizado"
            });
            return new Response<string>(true, "Usuario actualizado", null, (int)HttpStatusCode.OK);
        }
    }
}