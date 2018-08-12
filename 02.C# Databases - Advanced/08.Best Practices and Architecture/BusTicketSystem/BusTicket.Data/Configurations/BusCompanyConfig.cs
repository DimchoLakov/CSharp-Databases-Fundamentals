using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class BusCompanyConfig : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.BusCompanyId);

            modelBuilder
                .Property(x => x.Name)
                .IsRequired(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Nationality)
                .IsRequired(true)
                .HasMaxLength(64);

            modelBuilder
                .Property(x => x.Rating)
                .IsRequired(false);

            modelBuilder
                .HasMany(x => x.Reviews)
                .WithOne(x => x.BusCompany)
                .HasForeignKey(x => x.BusCompanyId);

            modelBuilder
                .HasMany(x => x.Trips)
                .WithOne(x => x.BusCompany)
                .HasForeignKey(x => x.BusCompanyId);
        }
    }
}
