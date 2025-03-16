using System.Text.Json.Serialization;

namespace UserService.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int RoleId { get; set; }
        public Roles Roles { get; set; }

        public UserDetails UserDetails { get; set; }

        [JsonIgnore]
        public ICollection<UserLogs> UserLogs { get; set; }
    }
}