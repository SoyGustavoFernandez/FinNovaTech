using System.Text.Json.Serialization;

namespace FinNovaTech.User.Domain.Entities
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
        public Role Role { get; set; }

        public UserDetail UserDetail { get; set; }

        [JsonIgnore]
        public ICollection<UserLog> UserLogs { get; set; }
    }
}