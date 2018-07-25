using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfigurations
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.Id);

            modelBuilder
                .Property(x => x.Type)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.UserId)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.User)
                .WithMany(x => x.PaymentMethods)
                .HasForeignKey(x => x.UserId);

            modelBuilder
                .Property(x => x.BankAccountId)
                .IsRequired(false);
            
            modelBuilder
                .Property(x => x.CreditCardId)
                .IsRequired(false);

            modelBuilder
                .HasOne(x => x.CreditCard)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey<PaymentMethod>(x => x.CreditCardId);

            modelBuilder
                .HasOne(x => x.BankAccount)
                .WithOne(x => x.PaymentMethod)
                .HasForeignKey<PaymentMethod>(x => x.BankAccountId);

            modelBuilder
                .HasIndex(x => new { x.UserId, x.BankAccountId, x.CreditCardId })
                .IsUnique(true);
        }
    }
}
