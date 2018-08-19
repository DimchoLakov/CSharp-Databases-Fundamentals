using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(x => x.CustomerId);

            builder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.IsYoungDriver)
                .HasDefaultValue(false);

            builder
                .Property(x => x.DateOfBirth)
                .IsRequired(true);
        }
    }
}
