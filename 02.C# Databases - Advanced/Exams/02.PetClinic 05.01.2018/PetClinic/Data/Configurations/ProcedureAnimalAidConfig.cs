using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class ProcedureAnimalAidConfig : IEntityTypeConfiguration<ProcedureAnimalAid>
    {
        public void Configure(EntityTypeBuilder<ProcedureAnimalAid> builder)
        {
            builder
                .HasKey(x => new { x.AnimalAidId, x.ProcedureId });

            builder
                .HasOne(x => x.AnimalAid)
                .WithMany(x => x.AnimalAidProcedures)
                .HasForeignKey(x => x.AnimalAidId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Procedure)
                .WithMany(x => x.ProcedureAnimalAids)
                .HasForeignKey(x => x.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
