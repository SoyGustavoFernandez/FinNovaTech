using MediatR;
using System.Net;

namespace UserService.Application.Commands.Users
{
    public class UpdateUserCommand : IRequest<HttpStatusCode>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }

        public UpdateUserCommand(int id, string name, string email, int role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }
    }
}