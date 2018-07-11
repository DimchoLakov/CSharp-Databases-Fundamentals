using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.VisitationId);

            modelBuilder
                .Property(p => p.Date)
                .IsRequired(true);

            modelBuilder
                .Property(p => p.Comments)
                .HasMaxLength(250)
                .IsUnicode(true);
        }
    }
}
