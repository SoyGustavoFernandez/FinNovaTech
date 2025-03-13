using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Users;

namespace UserService.Application.Queries.Users.Handlers
{
    /// <summary>
    /// Handler para obtener todos los usuarios.
    /// </summary>
    public class GetUserAllHandler : IRequestHandler<GetAllUsersQuery, ResponseDTO<List<UserDTO>>>
    {
        private readonly IUserRepository _repository;

        public GetUserAllHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<List<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetUsersAsync();
            return new ResponseDTO<List<UserDTO>>(true, "Usuarios encontrados", users, (int)HttpStatusCode.OK);
        }
    }
}