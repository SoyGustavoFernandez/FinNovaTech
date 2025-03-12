namespace UserService.Domain.Entities
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}