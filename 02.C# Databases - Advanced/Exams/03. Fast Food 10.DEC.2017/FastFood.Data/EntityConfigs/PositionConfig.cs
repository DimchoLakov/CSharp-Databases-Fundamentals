using FastFood.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFood.Data.EntityConfigs
{
    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
