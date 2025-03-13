using MediatR;
using System.Net;
using UserService.Application.Commands.Users;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Application.Commands.Users.Handlers
{
    /// <summary>
    /// Handler para eliminar un usuario.
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseDTO<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserLogRepository _userLogRepository;

        public DeleteUserHandler(IUserRepository repository, IUserLogRepository userLogRepository)
        {
            _repository = repository;
            _userLogRepository = userLogRepository;
        }

        public async Task<ResponseDTO<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDTO<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteUserAsync(user);
            await _userLogRepository.AddLogAsync(new UserLogs
            {
                UserId = user.Id,
                Action = "Usuario eliminado",
            });
            return new ResponseDTO<string>(true, "Usuario eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}