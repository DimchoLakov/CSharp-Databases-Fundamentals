using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfigurations
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.CreditCardId);

            modelBuilder
                .Property(x => x.Limit)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.MoneyOwed)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.ExpirationDate)
                .IsRequired(true);

            modelBuilder
                .Ignore(x => x.LimitLeft);
        }
    }
}
