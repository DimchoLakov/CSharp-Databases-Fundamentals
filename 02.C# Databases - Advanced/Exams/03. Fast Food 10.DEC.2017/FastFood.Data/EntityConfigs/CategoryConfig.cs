using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.EntityConfigs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
