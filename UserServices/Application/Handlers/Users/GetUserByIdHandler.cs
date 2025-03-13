using MediatR;
using System.Net;
using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Users;

namespace UserService.Application.Handlers.Users
{
    /// <summary>
    /// Handler para obtener un usuario por su identificador.
    /// </summary>
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResponseDTO<UserDTO>>
    {
        private readonly IUserRepository _repository; 

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDTO<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                return new ResponseDTO<UserDTO>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }

            var userDTO = new UserDTO { Name = user.Name, Email = user.Email };

            return new ResponseDTO<UserDTO>(true, "Usuario encontrado", userDTO, (int)HttpStatusCode.OK);
        }
    }
}