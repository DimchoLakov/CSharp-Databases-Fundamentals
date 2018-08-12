using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.BankAccountId);

            modelBuilder
                .Property(x => x.AccountNumber)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Balance)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.CustomerId)
                .IsRequired(false);
        }
    }
}
