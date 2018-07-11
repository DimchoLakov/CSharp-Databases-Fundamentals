using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.ProductId);

            modelBuilder
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Quantity)
                .IsRequired(true);

            modelBuilder
                .Property(p => p.Price)
                .IsRequired(true);

            modelBuilder
                .Property(p => p.Description)
                .HasMaxLength(250)
                .IsUnicode(true)
                .HasDefaultValue("No description");
        }
    }
}
