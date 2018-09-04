using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetClinic.Models;

namespace PetClinic.Data.Configurations
{
    public class PassportConfig : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> builder)
        {
            builder
                .HasKey(x => x.SerialNumber);

            builder
                .Property(x => x.OwnerPhoneNumber)
                .IsRequired();

            builder
                .Property(x => x.OwnerName)
                .IsRequired();

            builder
                .Property(x => x.RegistrationDate)
                .IsRequired();
        }
    }
}
