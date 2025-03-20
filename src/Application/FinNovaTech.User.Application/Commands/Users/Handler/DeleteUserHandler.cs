using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.Interfaces;
using FinNovaTech.User.Domain.Entities;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Commands.Users.Handler
{
    /// <summary>
    /// Handler para eliminar un usuario.
    /// </summary>
    public class DeleteUserHandler(IUserRepository repository, IUserLogRepository userLogRepository) : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IUserLogRepository _userLogRepository = userLogRepository;

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserEntityByIdAsync(request.Id);
            if (user == null)
            {
                return new Response<string>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }
            await _repository.DeleteUserAsync(user);
            await _userLogRepository.AddLogAsync(new UserLog
            {
                UserId = user.Id,
                Action = "Usuario eliminado",
            });
            return new Response<string>(true, "Usuario eliminado correctamente", null, (int)HttpStatusCode.OK);
        }
    }
}