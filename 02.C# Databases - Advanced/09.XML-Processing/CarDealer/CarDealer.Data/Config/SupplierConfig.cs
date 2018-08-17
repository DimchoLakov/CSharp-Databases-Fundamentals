using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class SupplierConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder
                .HasKey(x => x.SupplierId);

            builder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.IsImporter)
                .HasDefaultValue(false);

            builder
                .HasMany(x => x.Parts)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);
        }
    }
}
