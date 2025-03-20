using FinNovaTech.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Auth.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<User> AuthUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();

            modelBuilder.Entity<RefreshToken>().HasKey(x => x.Id);
            modelBuilder.Entity<RefreshToken>().HasOne(x => x.AuthUser).WithMany().HasForeignKey(x => x.AuthUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}