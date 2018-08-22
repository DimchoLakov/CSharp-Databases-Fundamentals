using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class VetConfig : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Profession)
                .IsRequired();

            builder
                .Property(x => x.Age)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            builder
                .HasIndex(x => x.PhoneNumber)
                .IsUnique();

            builder
                .HasMany(x => x.Procedures)
                .WithOne(x => x.Vet)
                .HasForeignKey(x => x.VetId);
        }
    }
}
