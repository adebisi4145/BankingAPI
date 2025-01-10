using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Context
{
    public class BankingDbContext: DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id); 
                entity.Property(a => a.AccountName)
                    .IsRequired()
                    .HasMaxLength(10); 
                entity.Property(a => a.Email)
                    .IsRequired();
                entity.Property(a => a.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(a => a.AccountBalance)
                    .HasDefaultValue(0)
                    .IsRequired(); 
            });

           
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique(); 
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.AccountNumber)
                .IsUnique();
        }


        public DbSet<Account> Accounts { get; set; }
    }
}
