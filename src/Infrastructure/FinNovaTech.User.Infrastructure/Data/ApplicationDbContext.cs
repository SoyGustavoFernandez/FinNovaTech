using userEntities = FinNovaTech.User.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.User.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<userEntities.User> Users { get; set; }
        public DbSet<userEntities.Role> Roles { get; set; }
        public DbSet<userEntities.UserDetail> UserDetails { get; set; }
        public DbSet<userEntities.UserLog> UserLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<userEntities.User>().HasKey(pk => pk.Id);
            modelBuilder.Entity<userEntities.User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<userEntities.User>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<userEntities.User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<userEntities.User>().Property(x => x.PasswordHash).IsRequired();
            modelBuilder.Entity<userEntities.User>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();

            //Relación con Roles
            modelBuilder.Entity<userEntities.User>().HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
            //Relación con UserDetails
            modelBuilder.Entity<userEntities.User>().HasOne(x => x.UserDetail).WithOne(x => x.User).HasForeignKey<userEntities.UserDetail>(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            //Relación con UserLogs
            modelBuilder.Entity<userEntities.User>().HasMany(x => x.UserLogs).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<userEntities.Role>().HasKey(pk => pk.Id);
            modelBuilder.Entity<userEntities.Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<userEntities.Role>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<userEntities.UserDetail>().HasKey(pk => pk.Id);
            modelBuilder.Entity<userEntities.UserDetail>().Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<userEntities.UserDetail>().Property(x => x.LastName).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<userEntities.UserDetail>().Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<userEntities.UserDetail>().Property(x => x.Address).IsRequired().HasMaxLength(255);

            modelBuilder.Entity<userEntities.UserLog>().HasKey(pk => pk.Id);
            modelBuilder.Entity<userEntities.UserLog>().Property(x => x.Action).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<userEntities.UserLog>().Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}