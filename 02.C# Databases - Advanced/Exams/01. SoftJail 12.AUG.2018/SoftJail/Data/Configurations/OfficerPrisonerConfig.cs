using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftJail.Data.Models;

namespace SoftJail.Data.Configurations
{
    public class OfficerPrisonerConfig : IEntityTypeConfiguration<OfficerPrisoner>
    {
        public void Configure(EntityTypeBuilder<OfficerPrisoner> builder)
        {
            builder
                .HasKey(x => new { x.OfficerId, x.PrisonerId });

            builder
                .Property(x => x.OfficerId) //?
                .IsRequired();

            builder
                .Property(x => x.PrisonerId) //?
                .IsRequired();

            builder
                .HasOne(x => x.Prisoner)
                .WithMany(x => x.PrisonerOfficers)
                .HasForeignKey(x => x.PrisonerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Officer)
                .WithMany(x => x.OfficerPrisoners)
                .HasForeignKey(x => x.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
