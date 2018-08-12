using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.CustomerId);

            modelBuilder
                .Property(x => x.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(32);

            modelBuilder
                .Property(x => x.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(32);

            modelBuilder
                .Property(x => x.DateOfBirth)
                .IsRequired(false);

            modelBuilder
                .Property(x => x.Gender)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.HomeTownId)
                .IsRequired(false);

            modelBuilder
                .HasOne(x => x.HomeTown)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.HomeTownId);

            modelBuilder
                .HasMany(x => x.Tickets)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder
                .Property(x => x.BankAccountId)
                .IsRequired(false);

            modelBuilder
                .HasOne(x => x.BankAccount)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>(x => x.BankAccountId);
        }
    }
}
