using FinNovaTech.Transaction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinNovaTech.Transaction.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<TransactionEvent> TransactionEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionEvent>().HasKey(e => e.Id);
            modelBuilder.Entity<TransactionEvent>().HasIndex(e => e.AccountId);
            modelBuilder.Entity<TransactionEvent>().Property(x => x.Timestamp).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<TransactionEvent>().Property(e => e.Amount).HasColumnType("decimal(18,2)");
        }
    }
}