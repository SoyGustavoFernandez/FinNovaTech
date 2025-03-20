using accountEntity = FinNovaTech.Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Account.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<accountEntity.Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<accountEntity.Account>().HasKey(a => a.Id);
            modelBuilder.Entity<accountEntity.Account>().Property(a => a.Balance).HasDefaultValue(0).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<accountEntity.Account>().Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}