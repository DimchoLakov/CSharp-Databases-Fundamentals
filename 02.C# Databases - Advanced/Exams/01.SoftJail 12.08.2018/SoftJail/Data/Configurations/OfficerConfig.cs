using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftJail.Data.Models;

namespace SoftJail.Data.Configurations
{
    public class OfficerConfig : IEntityTypeConfiguration<Officer>
    {
        public void Configure(EntityTypeBuilder<Officer> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.FullName)
                .IsRequired();

            builder
                .Property(x => x.Salary)
                .IsRequired();

            builder
                .Property(x => x.Position)
                .IsRequired();

            builder
                .Property(x => x.Weapon)
                .IsRequired();

            builder
                .Property(x => x.DepartmentId) //?
                .IsRequired();

            builder
                .HasOne(x => x.Department);
        }
    }
}
