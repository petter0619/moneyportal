using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .HasIndex(u => u.AuthId)
                .IsUnique();
        }
    }
}