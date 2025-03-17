using AccountService.Domain.Enums;

namespace AccountService.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}