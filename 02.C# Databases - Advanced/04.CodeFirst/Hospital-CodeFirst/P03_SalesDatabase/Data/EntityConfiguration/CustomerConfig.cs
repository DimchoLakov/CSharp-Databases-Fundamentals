using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.CustomerId);

            modelBuilder
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Email)
                .IsRequired(true)
                .HasMaxLength(80)
                .IsUnicode(false);

            modelBuilder
                .Property(p => p.CreditCardNumber)
                .IsUnicode(false);
        }
    }
}
