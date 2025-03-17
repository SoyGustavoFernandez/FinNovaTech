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
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, ResponseDto<string>>
    {
        private readonly IUserRepository _repository;
        private readonly IUserLogRepository _userLogRepository;

        public DeleteUserHandler(IUserRepository repository, IUserLogRepository userLogRepository)
        {
            _repository = repository;
            _userLogRepository = userLogRepository;
        }

        public async Task<ResponseDto<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserEntityByIdAsync(request.Id);
            if (user == null)
            {
                return new ResponseDto<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteUserAsync(user);
            await _userLogRepository.AddLogAsync(new UserLogs
            {
                UserId = user.Id,
                Action = "Usuario eliminado",
            });
            return new ResponseDto<string>(true, "Usuario eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}