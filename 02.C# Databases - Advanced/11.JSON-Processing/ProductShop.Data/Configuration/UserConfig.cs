using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FirstName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(64);

            builder
                .Property(x => x.LastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(64);
        }
    }
}
