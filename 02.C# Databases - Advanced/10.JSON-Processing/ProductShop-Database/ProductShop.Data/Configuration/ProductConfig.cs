using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            builder
                .Property(x => x.Price)
                .IsRequired();

            builder
                .HasOne(x => x.Buyer)
                .WithMany(x => x.ProductsBought)
                .HasForeignKey(x => x.BuyerId);

            builder
                .HasOne(x => x.Seller)
                .WithMany(x => x.ProductsSold)
                .HasForeignKey(x => x.SellerId);
        }
    }
}
