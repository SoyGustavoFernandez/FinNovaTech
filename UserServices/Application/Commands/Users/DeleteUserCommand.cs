using MediatR;
using System.Net;

namespace UserService.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<HttpStatusCode>
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}