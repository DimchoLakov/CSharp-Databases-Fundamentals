using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.TownId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Country)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(64);

            modelBuilder
                .HasMany(x => x.BusStations)
                .WithOne(x => x.Town);
        }
    }
}
