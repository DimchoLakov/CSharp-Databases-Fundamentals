using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data.EntityConfigurations;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data
{
    public class BillsPaymentSystemContext : DbContext
    {
        public BillsPaymentSystemContext()
        {
        }

        public BillsPaymentSystemContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //configure your server name in ConnectionString
                optionsBuilder.UseSqlServer(DbContextConfiguration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new BankAccountConfig())
                .ApplyConfiguration(new CreditCardConfig())
                .ApplyConfiguration(new PaymentMethodConfig())
                .ApplyConfiguration(new UserConfig());
        }
    }
}
