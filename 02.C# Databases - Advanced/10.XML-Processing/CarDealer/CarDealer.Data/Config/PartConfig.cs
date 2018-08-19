using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class PartConfig : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder
                .HasKey(x => x.PartId);

            builder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(128);

            builder
                .Property(x => x.Price)
                .IsRequired(true);

            builder
                .Property(x => x.Quantity)
                .IsRequired(true);
        }
    }
}
