using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para actualizar un usuario.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ResponseDTO<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserValidation _userValidation;

        public UpdateUserHandler(IUserRepository repository, IUserValidation userValidation)
        {
            _repository = repository;
            _userValidation = userValidation;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            bool validEmail = await _userValidation.ValidateUserFormatEmailAsync(request.Email);
            if (!validEmail)
            {
                return new ResponseDTO<string>(false, "Formato de correo incorrecto", null, (int)HttpStatusCode.BadRequest);
            }
            var emailExists = await _repository.EmailExistsAsync(request.Email);
            if (emailExists)
            {
                return new ResponseDTO<string>(false, "El email ya está registrado", null, (int)HttpStatusCode.BadRequest);
            }
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = request.Role;

            await _repository.UpdateUser(user);
            return new ResponseDTO<string>(true, "Usuario actualizado", null, (int)HttpStatusCode.OK);
        }
    }
}