using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para eliminar un usuario.
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseDTO<string>>
    {
        private readonly IUserRepository _repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteUserAsync(user);
            return new ResponseDTO<string>(true, "Usuario eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}