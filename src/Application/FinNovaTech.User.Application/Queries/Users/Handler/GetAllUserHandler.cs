using FinNovaTech.Common.Domain.Entities;
using FinNovaTech.User.Application.DTOs;
using FinNovaTech.User.Application.Interfaces;
using MediatR;
using System.Net;

namespace FinNovaTech.User.Application.Queries.Users.Handler
{
    /// <summary>
    /// Handler para obtener todos los usuarios.
    /// </summary>
    public class GetAllUserHandler : IRequestHandler<GetAllUsersQuery, Response<List<UserDto>>>
    {
        private readonly IUserRepository _repository;

        public GetAllUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repository.GetAllUsersAsync();
            return new Response<List<UserDto>>(true, "Usuarios encontrados", users, (int)HttpStatusCode.OK);
        }
    }
}