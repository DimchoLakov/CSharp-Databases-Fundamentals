using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class CarPartConfig : IEntityTypeConfiguration<CarPart>
    {
        public void Configure(EntityTypeBuilder<CarPart> builder)
        {
            builder
                .HasKey(x => new { x.CarId, x.PartId });

            builder
                .HasOne(x => x.Part)
                .WithMany(x => x.CarParts)
                .HasForeignKey(x => x.PartId);

            builder
                .HasOne(x => x.Car)
                .WithMany(x => x.CarParts)
                .HasForeignKey(x => x.CarId);
        }
    }
}
