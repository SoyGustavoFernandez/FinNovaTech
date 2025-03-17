using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Users;

namespace UserService.Application.Queries.Users.Handlers
{
    /// <summary>
    /// Handler para obtener un usuario por su identificador.
    /// </summary>
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResponseDto<UserDto>>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                return new ResponseDto<UserDto>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }

            var UserDto = new UserDto { Name = user.Name, Email = user.Email };

            return new ResponseDto<UserDto>(true, "Usuario encontrado", UserDto, (int)HttpStatusCode.OK);
        }
    }
}