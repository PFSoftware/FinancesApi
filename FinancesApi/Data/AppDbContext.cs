using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Models.Domain;

namespace PFSoftware.FinancesApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        public DbSet<MajorCategory> MajorCategories { get; set; }

        public DbSet<MinorCategory> MinorCategories { get; set; }

        public DbSet<Payee> Payees { get; set; }
    }
}