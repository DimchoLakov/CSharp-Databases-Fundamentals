using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductShop.Models;

namespace ProductShop.Data.Configuration
{
    public class CategoryProductConfig : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder
                .HasKey(x => new { x.CategoryId, x.ProductId });

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.CategoryProducts)
                .HasForeignKey(x => x.CategoryId);

            builder
                .HasOne(x => x.Product)
                .WithMany(x => x.CategoryProducts)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
