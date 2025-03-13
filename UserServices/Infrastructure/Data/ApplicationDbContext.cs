using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<UserLogs> UserLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(pk => pk.Id);
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();

            //Relación con Roles
            modelBuilder.Entity<User>().HasOne(x => x.Roles).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
            //Relación con UserDetails
            modelBuilder.Entity<User>().HasOne(x => x.UserDetails).WithOne(x => x.User).HasForeignKey<UserDetails>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            //Relación con UserLogs
            modelBuilder.Entity<User>().HasMany(x => x.UserLogs).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Roles>().HasKey(pk => pk.Id);
            modelBuilder.Entity<Roles>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Roles>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<UserDetails>().HasKey(pk => pk.Id);
            modelBuilder.Entity<UserDetails>().Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<UserDetails>().Property(x => x.LastName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<UserDetails>().Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<UserDetails>().Property(x => x.Address).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<UserLogs>().HasKey(pk => pk.Id);
            modelBuilder.Entity<UserLogs>().Property(x => x.Action).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<UserLogs>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}