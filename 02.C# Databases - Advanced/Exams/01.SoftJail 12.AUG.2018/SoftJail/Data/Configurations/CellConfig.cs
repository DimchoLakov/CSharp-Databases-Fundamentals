using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftJail.Data.Models;

namespace SoftJail.Data.Configurations
{
    public class CellConfig : IEntityTypeConfiguration<Cell>
    {
        public void Configure(EntityTypeBuilder<Cell> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CellNumber)
                .IsRequired();

            builder
                .Property(x => x.HasWindow)
                .IsRequired();

            builder
                .Property(x => x.DepartmentId) //?
                .IsRequired();

            builder
                .HasOne(x => x.Department)
                .WithMany(x => x.Cells)
                .HasForeignKey(x => x.DepartmentId);

            builder
                .HasMany(x => x.Prisoners)
                .WithOne(x => x.Cell)
                .HasForeignKey(x => x.CellId);
        }
    }
}
