using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class AnimalAidConfig : IEntityTypeConfiguration<AnimalAid>
    {
        public void Configure(EntityTypeBuilder<AnimalAid> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.Price)
                .IsRequired();
        }
    }
}
