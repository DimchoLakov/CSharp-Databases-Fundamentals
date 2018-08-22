using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class AnimalConfig : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Type)
                .IsRequired();

            builder
                .Property(x => x.Age)
                .IsRequired();

            builder
                .Property(x => x.PassportSerialNumber)
                .IsRequired();

            builder
                .HasMany(x => x.Procedures)
                .WithOne(x => x.Animal)
                .HasForeignKey(x => x.AnimalId);

            builder
                .HasOne(x => x.Passport)
                .WithOne(x => x.Animal)
                .HasForeignKey<Animal>(x => x.PassportSerialNumber);
        }
    }
}
