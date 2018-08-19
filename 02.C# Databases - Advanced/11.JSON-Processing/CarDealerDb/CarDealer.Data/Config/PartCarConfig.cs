using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class PartCarConfig : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder
                .HasKey(x => new { x.CarId, x.PartId });

            builder
                .HasOne(x => x.Part)
                .WithMany(x => x.PartCars)
                .HasForeignKey(x => x.PartId);

            builder
                .HasOne(x => x.Car)
                .WithMany(x => x.PartCars)
                .HasForeignKey(x => x.CarId);
        }
    }
}
