using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.TripId);

            modelBuilder
                .Property(x => x.DepartureTime)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.ArrivalTime)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Status)
                .IsRequired(true);

            modelBuilder
                .HasOne(x => x.DestinationBusStation)
                .WithMany(x => x.DestionationTrips)
                .HasForeignKey(x => x.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .HasOne(x => x.OriginBusStation)
                .WithMany(x => x.ArrivalTrips)
                .HasForeignKey(x => x.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
