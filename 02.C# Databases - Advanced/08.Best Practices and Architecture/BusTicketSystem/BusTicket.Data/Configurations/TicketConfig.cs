using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.TicketId);

            modelBuilder
                .Property(x => x.Price)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Seat)
                .IsRequired(true);
        }
    }
}
