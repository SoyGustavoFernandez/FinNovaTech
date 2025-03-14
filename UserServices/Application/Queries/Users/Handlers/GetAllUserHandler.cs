using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Queries.Users.Handlers
{
    /// <summary>
    /// Handler para obtener todos los usuarios.
    /// </summary>
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, ResponseDTO<List<UserDTO>>>
    {
        private readonly IUserRepository _repository;

        public GetAllUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsersAsync();
            return new ResponseDTO<List<UserDTO>>(true, "Usuarios encontrados", users, (int)HttpStatusCode.OK);
        }
    }
}