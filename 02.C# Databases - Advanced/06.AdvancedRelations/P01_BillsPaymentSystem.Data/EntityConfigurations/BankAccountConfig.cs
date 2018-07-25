using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfigurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.BankAccountId);

            modelBuilder
                .Property(x => x.Balance)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.BankName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder
                .Property(x => x.SwiftCode)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}
