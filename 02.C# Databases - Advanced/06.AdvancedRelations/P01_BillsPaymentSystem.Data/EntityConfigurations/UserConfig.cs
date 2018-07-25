using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.UserId);

            modelBuilder
                .Property(x => x.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder
                .Property(x => x.LastName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder
                .Property(x => x.Email)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(80);

            modelBuilder
                .Property(x => x.Password)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(25);
        }
    }
}
