﻿namespace UserService.Domain.Entities
{
    public class UserLogs
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
     
        public int UserId { get; set; }
        public User User { get; set; }
    }
}