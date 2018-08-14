using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class BusStationConfig : IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.BusStationId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(64);

            modelBuilder
                .HasOne(x => x.Town)
                .WithMany(x => x.BusStations);
        }
    }
}
