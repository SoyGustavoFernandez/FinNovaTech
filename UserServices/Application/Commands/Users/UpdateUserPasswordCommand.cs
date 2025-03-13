using MediatR;
using System.Net;

namespace UserService.Application.Commands.Users
{
    public class UpdateUserPasswordCommand: IRequest<HttpStatusCode>
    {
        public int Id { get; set; }
        public string Password { get; set; }

        public UpdateUserPasswordCommand(int id, string password)
        {
            Id = id;
            Password = password;
        }
    }
}
