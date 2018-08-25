using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class ProcedureConfig : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Ignore(x => x.Cost);

            builder
                .Property(x => x.DateTime)
                .IsRequired();
        }
    }
}
