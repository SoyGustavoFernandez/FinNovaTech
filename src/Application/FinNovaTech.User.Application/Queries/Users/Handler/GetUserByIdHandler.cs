using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Queries.Users.Handler
{
    /// <summary>
    /// Handler para obtener un usuario por su identificador.
    /// </summary>
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                return new Response<UserDto>(false, "Usuario no encontrado", null, (int)HttpStatusCode.NotFound);
            }

            var UserDto = new UserDto { Name = user.Name, Email = user.Email };

            return new Response<UserDto>(true, "Usuario encontrado", UserDto, (int)HttpStatusCode.OK);
        }
    }
}