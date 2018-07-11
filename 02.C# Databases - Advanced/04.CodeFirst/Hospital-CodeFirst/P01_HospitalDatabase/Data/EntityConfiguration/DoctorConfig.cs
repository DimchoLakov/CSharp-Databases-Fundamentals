using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> modelBuilder)
        {
            modelBuilder
                .HasKey(p => p.DoctorId);

            modelBuilder
                .Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            modelBuilder
                .Property(p => p.Specialty)
                .IsRequired(true)
                .HasMaxLength(100)
                .IsUnicode(true);

            modelBuilder
                .HasMany(p => p.Visitations)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorId);
        }
    }
}
