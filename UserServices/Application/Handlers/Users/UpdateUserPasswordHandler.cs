using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para actualizar la contraseña de un usuario.
    /// </summary>
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, ResponseDTO<string>>
    {
        private readonly IUserRepository _repository;

        public UpdateUserPasswordHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<string>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            if(string.IsNullOrEmpty(request.Password))
            {
                return new ResponseDTO<string>(false, "La contraseña no puede estar vacía", null, (int)HttpStatusCode.BadRequest);
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _repository.UpdateUser(user);
            return new ResponseDTO<string>(true, "Contraseña actualizada correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}