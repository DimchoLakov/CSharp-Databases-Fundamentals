using BusTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusTicket.Data.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder
                .HasKey(x => x.ReviewId);

            modelBuilder
                .Property(x => x.Content)
                .IsUnicode(true)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.Grade)
                .IsRequired(true);

            modelBuilder
                .Property(x => x.DateOfPublishing)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
