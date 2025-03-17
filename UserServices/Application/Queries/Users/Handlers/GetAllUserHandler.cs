using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Queries.Users.Handlers
{
    /// <summary>
    /// Handler para obtener todos los usuarios.
    /// </summary>
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, ResponseDto<List<UserDto>>>
    {
        private readonly IUserRepository _repository;

        public GetAllUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsersAsync();
            return new ResponseDto<List<UserDto>>(true, "Usuarios encontrados", users, (int)HttpStatusCode.OK);
        }
    }
}