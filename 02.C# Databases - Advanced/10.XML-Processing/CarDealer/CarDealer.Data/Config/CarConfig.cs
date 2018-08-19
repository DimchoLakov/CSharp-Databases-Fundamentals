using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data.Config
{
    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasKey(x => x.CarId);

            builder
                .Property(x => x.Make)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.Model)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            builder
                .Property(x => x.TravelledDistance)
                .IsRequired(true);

            builder
                .Ignore(x => x.CarPrice); //Ignore property (calculated property)
        }
    }
}
